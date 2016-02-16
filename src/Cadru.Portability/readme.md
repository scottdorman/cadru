To reproduce the overload resolution error in the SL50 target, change the code in https://github.com/scottdorman/cadru/blob/netcore/src/Cadru.Portability/InteropServices/MarshalShim.cs#L11 from 

```
        public static T PtrToStructure<T>(IntPtr ptr)
        {
        #if NET40 || NET45 || DOTNET5_1 || WP80
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        #elif PCL || SL50
            throw new PlatformNotSupportedException();
        #else
            return Marshal.PtrToStructure<T>(ptr);
        #endif
        }
```

to

```
        public static T PtrToStructure<T>(IntPtr ptr)
        {
        #if NET40 || NET45 || DOTNET5_1 || SL50 || WP80
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        #elif PCL
            throw new PlatformNotSupportedException();
        #else
            return Marshal.PtrToStructure<T>(ptr);
        #endif
        }
```

The compiler should choose the `object Marshal.PtrToStructure(IntPtr, Type)` overload but it's choosing `void Marshal.PtrToStructure(IntPtr, object)` instead.