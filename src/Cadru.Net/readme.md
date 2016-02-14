The WPA81 profile that causes the `CS0012` compiler error looks like

```
    "wpa81": {
      "compilationOptions": {
        "define": [ "WP" ]
      },
      "frameworkAssemblies": {
        "mscorlib": "4.0.0.0",
        "System": "4.0.0.0",
        "System.Core": "4.0.0.0",
        "System.Runtime": "4.0.10.0",
        "System.Globalization": "4.0.0.0",
        "System.Diagnostics.Contracts": "4.0.0.0",
        "System.Diagnostics.Tools": "4.0.0.0",
        "System.Diagnostics.Debug": "4.0.0.0",
        "System.Runtime.Serialization": "4.0.0.0",
        "System.Runtime.Serialization.Primitives": "4.0.0.0",
        "System.Linq": "4.0.0.0",
        "System.Runtime.Extensions": "4.0.0.0",
        "System.Collections": "4.0.0.0",
        "System.Collections.Concurrent": "4.0.0.0",
        "System.Text.RegularExpressions": "4.0.0.0",
        "System.Text.Encoding": "4.0.0.0",
        "System.Reflection": "4.0.0.0",
        "System.Reflection.Extensions": "4.0.0.0",
        "System.Resources.ResourceManager": "4.0.0.0",
        "System.ComponentModel": "4.0.0.0",
        "System.Net.Http": "4.0.0.0",
        "System.Net.Primitives": "4.0.0.0",
        "System.Net.Requests": "4.0.0.0",
        "System.Threading": "4.0.0.0"
      },
      "dependencies": {
      }
    },
```

I really want to just target profile 328:

```
    ".NETPortable,Version=v4.0,Profile=Profile328": {
      "compilationOptions": {
      },
      "frameworkAssemblies": {
        "mscorlib": { },
        "System": { },
        "System.Core": { },
        "System.Net": { },
        "System.Runtime.Serialization": { }
      },
      "dependencies": {
      }
    },
```

instead of explicitly targeting frameworks.