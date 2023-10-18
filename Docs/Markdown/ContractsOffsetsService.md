# Contracts Offsets Service (REST)
The Relativity Contracts application uses offset data to link field values to text in a contract document. Offsets can be created through the Contracts Viewer using the Send To Field feature, but they can also be created and managed through the Contracts Offsets Service REST API.

The Contracts Offsets Service exposes CRUD endpoints that you can use to programmatically manipulate offsets in your Relativity environment.

Some use cases for the Contracts Offsets Service include:
* Creating offsets based on a custom model so that they can be viewed in the Contracts Viewer and possibly edited by reviewers.
* Extracting reviewer-edited offsets from contracts to train a custom model.

## Guidelines for the Contracts Offsets Service
What follows are general guidelines for working with this service.

### URLs
The URLs for the Contracts Offsets Service's REST endpoints contain path parameters that you need to set before making a call:
* Set the **{versionNumber}** placeholder to the version of the REST API that you want to use, using the format of lowercase *v* followed by the *version number* (e.g. *v1* or *v2*).
* Set the **{workspaceId}** and **{documentId}** path parameters to the Artifact ID of the given entity. For example, you'd set **{workspaceId}** to the Artifact ID of the Workspace.

For example, you can use the following URL to retrieve the offsets associated with a contract document:
```
<host>/Relativity.Rest/API/contracts/{versionNumber}/offsets/{workspaceId}/document/{documentId}
```
You'd set the path parameters as follows:
* **{versionNumber}** to the version of the API, such as **v1**.
* **{workspaceId}** to the Artifact ID of the Workspace that contains the document.
* **{documentId}** to the Artifact ID of the document you want to retrieve offsets for.

## Client Code Example
To use the Contracts Offsets Service, send requests by making calls with the required HTTP methods.

You can download a .NET example project [here](https://raw.githubusercontent.com/relativitydev/relativity-contracts-documentation-public/main/Examples/OffsetsAndHOcrApis.zip).

## Create Offsets
To create one or more offsets, send a POST request with a URL in this format:
```
<host>/Relativity.Rest/API/contracts/{versionNumber}/offsets/{workspaceId}/document/{documentId}
```

<details>
<summary>View field descriptions for a request</summary>

The body of the request must contain an **Offsets** field, which is an array of objects defining the offsets you want to create.

An offset contains the following fields:
* **Id** - (Optional) An ID uniquely identifying the offset. To create a new offset, this should be omitted or set to `0` or `null`.
    * **IMPORTANT:** If you are intending to create a new offset, this should NEVER be set to anything other than `0` or `null`. Creating new offsets with specific IDs is not supported, and attempting to do so will result in either an existing offset being updated (if one with the *Id* already exists) or a new offset being created with an *Id* equal to the largest existing offset *Id*+1, NOT the *Id* specified.
* **DocumentId** - The Artifact ID of the document that the offset should be associated with. This must match the document ID in the URL.
* **FieldId** - The Artifact ID of the field the offset is associated with.
* **AssociatedArtifactId**<span id="associated-artifact-id-create-request-description"></span> - If the offset is associated with a multi-object field, the Artifact ID of an object to be selected. Otherwise, this should contain the same value as the *DocumentId* field.
    * To select more than one object, create multiple offsets with the same document ID and field ID, then set the *AssociatedArtifactId* field on each offset to the Artifact ID of the object you want to select.
* **ChoiceId** - (Optional) The Artifact ID of a choice to associate the offset with. This can be used to correlate different offsets that share the same document, field, and choice IDs.
* **Offset** - The position in the document where the offset's text starts, given in number of characters since the beginning of the document.
* **Length** - The text length of the offset, given in number of characters.
* **Height** - (Optional) The height of the offset's text as a percentage of the document image's height. The value can be either `null` or a positive decimal number.
* **Width** - (Optional) The width of the offset's text as a percentage of the document image's width. The value can be either `null` or a positive decimal number.
* **Left** - (Optional) How far from the left edge of the document image the start of the offset's text is as a percentage of the document image's width. The value can be either `null` or a a non-negative decimal number.
* **Top** - (Optional) How far from the top edge of the document image the start of the offset's text is as a percentage of the document image's height. The value can be either `null` or a non-negative decimal number.
* **PageNumber** - (Optional) The page number of the document where the offset's text is found. The value can be either `null` or a positive integer greater than or equal to 1.

<div style="background-color: #f0f7fb; border-left: solid 4px #3498db; overflow: hidden; padding: 0.6em; font-size: 1em; line-height: 1.5em; page-break-inside: avoid; color: #666666; font-weight: 400; font-family: proxima-nova, arial, sans-serif;">
<b>Note:</b> The position and size of an offset's associated text can be defined one of two ways:
<ol>
    <li>By providing values for <b>Offset</b> and <b>Length</b>.</li>
    <li>By providing values for <b>Height</b>, <b>Width</b>, <b>Left</b>, <b>Top</b>, and <b>PageNumber</b>.</li>
</ol>
If no value is provided for <b>Length</b>, values must be provided for <b>Height</b>, <b>Width</b>, <b>Left</b>, <b>Top</b>, and <b>PageNumber</b>.
</div>
</details>

<details>
<summary>View a sample JSON request</summary>

``` json
{
    "Offsets": [
        {
            "Id": 0,
            "DocumentId": 1041445,
            "FieldId": 1042006,
            "AssociatedArtifactId": 1041445,
            "ChoiceId": null,
            "Offset": 489,
            "Length": 211,
            "Height": null,
            "Width": null,
            "Left": null,
            "Top": null,
            "PageNumber": null
        },
        {
            "DocumentId": 1041445,
            "FieldId": 1042012,
            "AssociatedArtifactId": 1041445,
            "Offset": 14,
            "Length": 104,
        }
    ]
}
```
</details>

<details>
<summary>View field descriptions for a response</summary>

The body of the response contains an array of objects defining the offsets that were successfully created or updated (see [Upsert Offsets](#upsert-offsets) for more info on why the create endpoint works this way).

Each offset object in the response contains the following fields (fields marked with * will be omitted from an offset object if their value is `null`):
* **Id** - An ID uniquely identifying the offset.
* **DocumentId** - The Artifact ID of the document that the offset is associated with.
* **FieldId** - The Artifact ID of the field the offset is associated with.
* **AssociatedArtifactId** - If this field does not contain the same value as the *DocumentId* field, the offset is associated with a multi-object field and its value is the Artifact ID of a selected object. Otherwise, this contains the same value as the *DocumentId* field.
    * For more information on how this field behaves for multi-object fields, see [its description](#associated-artifact-id-create-request-description) in the section above about the create request.
* **ChoiceId*** - The Artifact ID of the choice the offset is associated with. If not set, its value is `null`.
  * This can be used to correlate different offsets that share the same document, field, and choice IDs.
* **Offset** - The position where the offset's text starts in the document, given in number of characters since the beginning of the document.
* **Length** - The text length of the offset, given in number of characters.
* **Height*** - The height of the offset's text as a percentage of the document image's height.
* **Width*** - The width of the offset's text as a percentage of the document image's width.
* **Left*** - How far from the left edge of the document image the start of the offset's text is as a percentage of the document image's width.
* **Top*** - How far from the top edge of the document image the start of the offset's text is as a percentage of the document image's height.
* **PageNumber*** - The page number of the document where the offset's text is found. Page numbers start at 1.
</details>

<details>
<summary>View a sample JSON response</summary>

``` json
[
    {
        "Id": 9,
        "DocumentId": 1041445,
        "FieldId": 1042006,
        "AssociatedArtifactId": 1041445,
        "Offset": 489,
        "Length": 211
    },
    {
        "Id": 10,
        "DocumentId": 1041445,
        "FieldId": 1042006,
        "AssociatedArtifactId": 1041445,
        "Offset": 14,
        "Length": 104
    }
]
```
</details>

## Read Offsets
To get the offsets associated with a particular document, send a GET request with a URL in the following format:
```
<host>/Relativity.Rest/API/contracts/{versionNumber}/offsets/{workspaceId}/document/{documentId}
```
The offset objects in the response for a read operation contain the same fields as those for a create response. See the field descriptions for the response in [Create Offsets](#create-offsets).

## Update Offsets
To update one or more offsets, send a POST request with a URL in this format:
```
<host>/Relativity.Rest/API/contracts/{versionNumber}/offsets/{workspaceId}/document/{documentId}
```

<details>
<summary>View field descriptions for a request</summary>

The body of the request must contain an **Offsets** field, which is an array of objects defining the offsets you want to update and the state you want them to be in after the update.

An offset contains the following fields:
* **Id** - An integer ID uniquely identifying the offset.
    * **IMPORTANT:** If no offset already exists with the specified *Id*, a new one will be created with an *Id* value equal to the largest existing offset *Id*+1, NOT the *Id* specified. If it's important to your use case that new offsets not be accidentally created this way, it's highly recommended you check which offsets already exist using the [read API](#read-offsets) before attempting to update.
* **DocumentId** - The Artifact ID of the document that the offset should be associated with. This must match the document ID in the URL.
* **FieldId** - The Artifact ID of the field the offset is associated with.
* **AssociatedArtifactId**<span id="associated-artifact-id-update-request-description"></span> - If the offset is associated with a multi-object field, the Artifact ID of an object to be selected. Otherwise, this should contain the same value as the *DocumentId* field.
    * For more information on how this works, see the request *AssociatedArtifactId* field description for the [Create API](#create-offsets).
* **ChoiceId** - (Optional) The Artifact ID of a choice to associate the offset with. This can be used to correlate different offsets that share the same document, field, and choice IDs.
* **Offset** - The position in the document where the offset's text starts, given in number of characters since the beginning of the document.
* **Length** - The text length of the offset, given in number of characters.
* **Height** - (Optional) The height of the offset's text as a percentage of the document image's height. The value can be either `null` or a positive decimal number.
* **Width** - (Optional) The width of the offset's text as a percentage of the document image's width. The value can be either `null` or a positive decimal number.
* **Left** - (Optional) How far from the left edge of the document image the start of the offset's text is as a percentage of the document image's width. The value can be either `null` or a a non-negative decimal number.
* **Top** - (Optional) How far from the top edge of the document image the start of the offset's text is as a percentage of the document image's height. The value can be either `null` or a non-negative decimal number.
* **PageNumber** - (Optional) The page number of the document where the offset's text is found. The value can be either `null` or a positive integer greater than or equal to 1.

<div style="background-color: #f0f7fb; border-left: solid 4px #3498db; overflow: hidden; padding: 0.6em; font-size: 1em; line-height: 1.5em; page-break-inside: avoid; color: #666666; font-weight: 400; font-family: proxima-nova, arial, sans-serif;">
<b>Note:</b> The position and size of an offset's associated text can be defined one of two ways:
<ol>
    <li>By providing values for <b>Offset</b> and <b>Length</b>.</li>
    <li>By providing values for <b>Height</b>, <b>Width</b>, <b>Left</b>, <b>Top</b>, and <b>PageNumber</b>.</li>
</ol>
If no value is provided for <b>Length</b>, values must be provided for <b>Height</b>, <b>Width</b>, <b>Left</b>, <b>Top</b>, and <b>PageNumber</b>.
</div>
</details>

<details>
<summary>View a sample JSON request</summary>

``` json
{
    "Offsets": [
        {
            "Id": 12,
            "DocumentId": 1041445,
            "FieldId": 1042006,
            "AssociatedArtifactId": 1041445,
            "ChoiceId": null,
            "Offset": 489,
            "Length": 211,
            "Height": null,
            "Width": null,
            "Left": null,
            "Top": null,
            "PageNumber": null
        },
        {
            "Id": 3,
            "DocumentId": 1041445,
            "FieldId": 1042012,
            "AssociatedArtifactId": 1041445,
            "ChoiceId": 1042316,
            "Offset": 14,
            "Length": 104,
        }
    ]
}
```
</details>

<details>
<summary>View field descriptions for a response</summary>
The body of the response contains an array of objects defining the offsets that were successfully created or updated (see [Upsert Offsets](#upsert-offsets) for more info on why the update endpoint works this way).

Each offset object in the response contains the following fields (fields marked with * will be omitted from an offset object if their value is `null`):
* **Id** - An ID uniquely identifying the offset.
* **DocumentId** - The Artifact ID of the document that the offset is associated with.
* **FieldId** - The Artifact ID of the field the offset is associated with.
* **AssociatedArtifactId** - If this field does not contain the same value as the *DocumentId* field, the offset is associated with a multi-object field and its value is the Artifact ID of a selected object. Otherwise, this contains the same value as the *DocumentId* field.
    * For more information on how this field behaves for multi-object fields, see [its description](#associated-artifact-id-update-request-description) in the section above about the request.
* **ChoiceId*** - The Artifact ID of the choice the offset is associated with. If not set, its value is `null`.
  * This can be used to correlate different offsets that share the same document, field, and choice IDs.
* **Offset** - The position where the offset's text starts in the document, given in number of characters since the beginning of the document.
* **Length** - The text length of the offset, given in number of characters.
* **Height*** - The height of the offset's text as a percentage of the document image's height.
* **Width*** - The width of the offset's text as a percentage of the document image's width.
* **Left*** - How far from the left edge of the document image the start of the offset's text is as a percentage of the document image's width.
* **Top*** - How far from the top edge of the document image the start of the offset's text is as a percentage of the document image's height.
* **PageNumber*** - The page number of the document where the offset's text is found. Page numbers start at 1.
</details>

<details>
<summary>View a sample JSON response</summary>

``` json
[
    {
        "Id": 9,
        "DocumentId": 1041445,
        "FieldId": 1042006,
        "AssociatedArtifactId": 1041445,
        "Offset": 489,
        "Length": 211
    },
    {
        "Id": 10,
        "DocumentId": 1041445,
        "FieldId": 1042006,
        "AssociatedArtifactId": 1041445,
        "Offset": 14,
        "Length": 104
    }
]
```
</details>

## Delete An Offset
To remove an offset from a document, send a DELETE request with a URL in the following format:
```
<host>/Relativity.Rest/API/contracts/{versionNumber}/offsets/{workspaceId}/document/{documentId}/{offsetId}
```
The **{offsetId}** parameter is the ID of the offset to be deleted. It can be found in the **Id** field of an offset in a read response. See [Read Offsets](#read-offsets).

Both the request and response bodies are empty. When an offset was successfully deleted, the response returns a status code of 200.

## Upsert Offsets
The Contracts Offsets Service's create and update operations use the same endpoint, and that endpoint upserts the requested offsets. This allows multiple offsets to upserted in the same request.

If a requested offset has an Id of `0` or `null`, or if the *Id* field is not included, a new offset will be created. 

If the offset has an *Id* > `0`, and the offset with that *Id* exists, the existing offset is updated to match what was requested.

The request and response structure are the same as those for the [Create](#create-offsets) and [Update](#update-offsets) APIs.

<div style="background-color: #f0f7fb; border-left: solid 4px #3498db; overflow: hidden; padding: 0.6em; font-size: 1em; line-height: 1.5em; page-break-inside: avoid; color: #666666; font-weight: 400; font-family: proxima-nova, arial, sans-serif;">
<b>IMPORTANT:</b> Use this API at your own risk!
<br/>
<br/>
For any given offset in the request body, if no offset already exists with the specified <i>Id</i>, a new one will be created with an <i>Id</i> value equal to the largest existing offset <i>Id</i>+1, NOT the <i>Id</i> specified. Creating a new offset with a specific <i>Id</i> is not supported.
<br/>
<br/>
It's highly recommended that you check which offsets already exist using the <a href="#read-offsets">Read API</a> before running a large upsert operation.
</div>
