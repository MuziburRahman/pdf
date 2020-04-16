IxMilia.Pdf
===========

A portable .NET library for creating simple PDF documents based off the Adobe
specification from [here](http://wwwimages.adobe.com/content/dam/Adobe/en/devnet/pdf/pdfs/PDF32000_2008.pdf).
This library is forked from [ixmilia/pdf](https://github.com/ixmilia/pdf) and modified a bit to use System.Numeric.Vector2 instead of Custom struct.

## Usage

Create a PDF file:

``` C#
using System.IO;
using IxMilia.Pdf;
// ...

// create the page
PdfPage page = PdfPage.NewLetter(); // 8.5" x 11"
PdfPoint topLeft = new Vector2(0, page.Height);
PdfPoint bottomLeft = new Vector2();
PdfPoint bottomRight = new Vector2(page.Width, 0);

// add text
page.Items.Add(new PdfText("some text", new PdfFontType1(PdfFontType1Type.Helvetica), 12.0, bottomLeft));

// add a line and circle
PdfPathBuilder builder = new PdfPathBuilder()
{
    new PdfLine(topLeft, bottomRight),
    new PdfCircle(new Vector2(200, 100), 50)
};
page.Items.Add(builder.ToPath());

// create the file and add the page
PdfFile file = new PdfFile();
file.Pages.Add(page);

file.Save(@"C:\Path\To\File.pdf");
```

License
===========
The MIT License (MIT)

Copyright (c) Muzibur Rahman.
All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.



