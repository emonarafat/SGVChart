# SGVChart C#
Create PIE Chart from Template and Data 

use only StringBuilder, no svg library,  output svg
```csharp
var svgTemplate = "<svg viewBox='-1 -1 2 2' style='transform: rotate(-90deg)'>{0}</svg>";
var values = new[] { 0.1, 0.65, 0.2 };
var labels = new[] { "Coral", "CornflowerBlue", "#00ab6b" };
var result = SVGCreator.CreateSVG(svgTemplate, values, labels);
```
