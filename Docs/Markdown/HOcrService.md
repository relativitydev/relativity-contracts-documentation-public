# hOCR Service (REST)
When a Contracts OCR Set is run, the Contracts OCR Agent creates Contracts Image Results objects for every page of every document in the Contracts OCR Set. Those Contracts Image Results objects contain hOCR data describing the content and structure of the text found in the document image via OCR.

<div style="background-color: #f0f7fb; border-left: solid 4px #3498db; overflow: hidden; padding: 0.6em; font-size: 1em; line-height: 1.5em; page-break-inside: avoid; color: #666666; font-weight: 400; font-family: proxima-nova, arial, sans-serif;">
<b>Note:</b> hOCR is an open data representation standard for formatted text obtained from OCR. You can find the specification for the hOCR microformat <a href="https://kba.github.io/hocr-spec/">here</a>.
</div>

The Contracts hOCR Service exposes an endpoint for reading hOCR data created by the Contracts OCR Agent.

## Guidelines for the hOCR Service
What follows are general guidelines for working with this service.

### URLs
The URLs for the hOCR Service's REST endpoints contain path parameters that you need to set before making a call:
* Set the **{versionNumber}** placeholder to the version of the REST API that you want to use, using the format of lowercase *v* followed by the *version number* (e.g. *v1* or *v2*).
* Set the **{workspaceId}** and **{documentId}** path parameters to the Artifact ID of the given entity. For example, you'd set **{workspaceId}** to the Artifact ID of the Workspace.

For example, you can use the following URL to retrieve the hOCR data for a particular page of a document:
```
<host>/Relativity.Rest/API/contracts/{versionNumber}/ocr/{workspaceId}/document/{documentId}/page{pageNumber}
```
You'd set the path parameters as follows:
* **{versionNumber}** to the version of hte API, such as **v1**.
* **{workspaceId}** to the Artifact ID of the Workspace that contains the document.
* **{documentId}** to the Artifact ID of the document you want to retrieve hOCR data for.
* **{pageNumber}** to the page number for the document image page you want to retrieve hOCR data for. Page numbers begin at 1.

<div style="background-color: #f0f7fb; border-left: solid 4px #3498db; overflow: hidden; padding: 0.6em; font-size: 1em; line-height: 1.5em; page-break-inside: avoid; color: #666666; font-weight: 400; font-family: proxima-nova, arial, sans-serif;">
<b>Note:</b> <b>pageNumber</b> is the page number in the document's <i>image</i>. This distinction is important if you're getting your page numbers from the Contracts Viewer, as the Contracts Text Viewer omits blank pages, while the Contracts Image Viewer does not.
</div>

## Client Code Example
To use the hOCR Service, send requests by making calls with the required HTTP methods.

`TODO` Put the link here once we have it.
You can download a .NET example project [here]().

## Read hOCR Data
To get hOCR data for a document page, send a GET request with a URL in this format:
```
<host>/Relativity.Rest/API/contracts/{versionNumber}/ocr/{workspaceId}/document/{documentId}/page{pageNumber}
```

<details>
<summary>View field descriptions for a response</summary>

* **DocumentId** - The Artifact ID of the document the hOCR data is associated with.
* **PageNumber** - The page number of the page in the document image that the hOCR data represents.
* **Text** - An array of objects representing the terms found in the document image by Contracts OCR. Each object has the following fields:
   * **Confidence** - The hOCR confidence rating (as a percentage) for the text. The value is always a non-negative integer between 0 and 100.
   * **Offset** - The position of the term (in number of characters) from the start of the document.
   * **Length** - The number of characters in the term's text, as outputted by the OCR engine.
   * **Text** - A string containing the term's text, as outputted by the OCR engine.
   * **BoundingBox** - An object representing a rectangular box that "bounds" the term in the document image. It's used to define the term's position and size in the document page image, and it has these fields:
     * **Left** - The bounding box's distance (in pixels) from the left edge of the document page image.
     * **Top** - The bounding box's distance (in pixels) from the top edge of the document page image.
     * **Width** - The width (in pixels) of the bounding box.
     * **Height** - The height (in pixels) of the bounding box.

</details>

<details>
<summary>View a sample JSON response</summary>

``` json
{
    "DocumentId": 1041438,
    "PageNumber": 2,
    "Text": [
        {
            "Confidence": 96,
            "Offset": 2,
            "Length": 7,
            "Text": "Exhibit",
            "BoundingBox": {
                "Left": 1760,
                "Top": 144,
                "Width": 94,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 10,
            "Length": 4,
            "Text": "10.5",
            "BoundingBox": {
                "Left": 1865,
                "Top": 144,
                "Width": 54,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 17,
            "Length": 7,
            "Text": "Summary",
            "BoundingBox": {
                "Left": 521,
                "Top": 219,
                "Width": 131,
                "Height": 29
            }
        },
        {
            "Confidence": 95,
            "Offset": 25,
            "Length": 2,
            "Text": "of",
            "BoundingBox": {
                "Left": 661,
                "Top": 219,
                "Width": 21,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 28,
            "Length": 6,
            "Text": "Fiscal",
            "BoundingBox": {
                "Left": 684,
                "Top": 219,
                "Width": 89,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 35,
            "Length": 4,
            "Text": "2008",
            "BoundingBox": {
                "Left": 784,
                "Top": 219,
                "Width": 66,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 40,
            "Length": 6,
            "Text": "Target",
            "BoundingBox": {
                "Left": 861,
                "Top": 220,
                "Width": 89,
                "Height": 28
            }
        },
        {
            "Confidence": 95,
            "Offset": 47,
            "Length": 10,
            "Text": "Short-Term",
            "BoundingBox": {
                "Left": 959,
                "Top": 219,
                "Width": 160,
                "Height": 23
            }
        },
        {
            "Confidence": 95,
            "Offset": 58,
            "Length": 9,
            "Text": "Incentive",
            "BoundingBox": {
                "Left": 1126,
                "Top": 219,
                "Width": 121,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 68,
            "Length": 11,
            "Text": "Percentages",
            "BoundingBox": {
                "Left": 1256,
                "Top": 220,
                "Width": 165,
                "Height": 28
            }
        },
        {
            "Confidence": 96,
            "Offset": 80,
            "Length": 3,
            "Text": "for",
            "BoundingBox": {
                "Left": 1434,
                "Top": 219,
                "Width": 38,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 84,
            "Length": 3,
            "Text": "the",
            "BoundingBox": {
                "Left": 1482,
                "Top": 220,
                "Width": 40,
                "Height": 22
            }
        },
        {
            "Confidence": 96,
            "Offset": 88,
            "Length": 5,
            "Text": "Named",
            "BoundingBox": {
                "Left": 656,
                "Top": 260,
                "Width": 94,
                "Height": 22
            }
        },
        {
            "Confidence": 96,
            "Offset": 94,
            "Length": 9,
            "Text": "Executive",
            "BoundingBox": {
                "Left": 756,
                "Top": 259,
                "Width": 126,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 104,
            "Length": 8,
            "Text": "Officers",
            "BoundingBox": {
                "Left": 891,
                "Top": 259,
                "Width": 110,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 113,
            "Length": 2,
            "Text": "of",
            "BoundingBox": {
                "Left": 1014,
                "Top": 259,
                "Width": 27,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 116,
            "Length": 6,
            "Text": "Lennox",
            "BoundingBox": {
                "Left": 1046,
                "Top": 260,
                "Width": 99,
                "Height": 22
            }
        },
        {
            "Confidence": 96,
            "Offset": 123,
            "Length": 13,
            "Text": "International",
            "BoundingBox": {
                "Left": 1154,
                "Top": 259,
                "Width": 174,
                "Height": 23
            }
        },
        {
            "Confidence": 96,
            "Offset": 137,
            "Length": 4,
            "Text": "Inc.",
            "BoundingBox": {
                "Left": 1339,
                "Top": 260,
                "Width": 48,
                "Height": 22
            }
        }
    ]
}
```
</details>