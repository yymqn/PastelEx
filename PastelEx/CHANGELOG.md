## Version 1.0.3
### Fixes
- Fixed DecorationCollection not applying colors properly to the console.
### New features
- Added PastelSettings class. You can change PastelEx behaviour by modifying PastelEx.Settings at runtime.
- New CompactColor struct, which can hold both ConsoleColor and Color. Used for PastelEx.Foreground and PastelEx.Background properties. Color and ConsoleColor can be implicitly converted into CompactColor and explicitly vice versa.