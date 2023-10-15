## Version 1.0.5
### News
- Removed support for 8bit colors.
- New PastelEx.Clear method (doesn't flicker)
- PastelEx.Refill is now obsolete, use PastelEx.Clear instead!

## Version 1.0.4
### Fixes
- Fixed that you had to explicitly cast CompactColor into one of Color or ConsoleColor, when using Fg() and Bg() methods.

## Version 1.0.3
### Fixes
- Fixed DecorationCollection not applying colors properly to the console.
### News
- Added PastelSettings class. You can change PastelEx behaviour by modifying PastelEx.Settings at runtime.
- New CompactColor struct, which can hold both ConsoleColor and Color. Used for PastelEx.Foreground and PastelEx.Background properties. Color and ConsoleColor can be implicitly converted into CompactColor and explicitly vice versa.