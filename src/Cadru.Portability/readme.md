# Warning NU1007
It looks like DNU/NuGet is picking up the wrong dependency. Even though I have v4.0.20 specified for `System.Runtime.InteropServices`, it's producing the following warning:

```
>C:\Users\Scott\Source\Repos\cadru\src\Cadru.Portability\project.json(57,43): 
warning NU1007: Dependency specified was System.Runtime.InteropServices >= 4.0.20 but ended up with System.Runtime.InteropServices 4.0.21-beta-23516.
```

However, if I explicitly target that version, then the project fails to compile with the following error:

```
1>C:\Users\Scott\Source\Repos\cadru\src\Cadru.Portability\InteropServices\MarshalShim.cs(18,20): WindowsPhoneApp,Version=v8.1 error CS0103: The name 'Marshal' does not exist in the current context
```

This makes it appear as though the `Marshal` class no longer exists in the later version of the package, even though it seems to be perfectly happy when it implicitly picks up that version.

# Overload resolution error

To reproduce the overload resolution error in the SL50 target, change the code in https://github.com/scottdorman/cadru/blob/netcore/src/Cadru.Portability/InteropServices/MarshalShim.cs#L11 from 

```c#
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

```c#
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