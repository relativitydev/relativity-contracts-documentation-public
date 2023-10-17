using System;
using System.Threading.Tasks;

namespace ContractsOffsetsApiExamples
{
	internal class Program
	{
		static void Main(string[] args)
		{
			/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
			 * HOW TO USE THIS EXAMPLE PROJECT                               *
			 * ------------------------------------------------------------- *
			 * 1. Make sure you've followed the steps in ExampleSetup.html   *
			 *    to set up the Workspace you'll be making requests against. *
			 * 2. Update the values being assigned to the constants inside   *
			 *    the "Request Configuration" region below (you might need   *
			 *    to expand it to see them) so that they match your          *
			 *    environment and Workspace.                                 *
			 * 3. All request examples are commented out by default, and     *
			 *    each is in its own region below. If you want to run one,   *
			 *    open its region and uncomment it (most IDEs have a         *
			 *    keystroke or command for this).                            *
			 *                                                               *
			 * NOTE: The request URLs can be found in the OffsetApiHelper    *
			 *    and HOcrApiHelper classes. Relative URLs are used in the   *
			 *    methods making the requests. These get appended to the     *
			 *    base URL defined in each class's constructor.              *
			 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

			#region Request Configuration

			// The hostname of the environment hosting the target Relativity instance.
			//
			// For example, if you'd normally go to https://example.com/relativity/ in your browser
			// to view the Relativity UI, you'd set this to "example.com".
			const string HOSTNAME = "localhost";

			// The username to use for logging into the target environment.
			const string USERNAME = "relativity.admin@kcura.com";

			// The password associated with that username.
			const string PASSWORD = "";

			// The ID of the Workspace you set up by following the manual steps.
			const int WORKSPACE_ID = 0;

			// The Artifact ID of the Document you'd like to interact with.
			//
			// The Document must have been successfully imaged and Contracts OCR'ed.
			const int DOCUMENT_ID = 0;

			// The Artifact ID of the Field the created/updated offset should be associated with.
			const int FIELD_ID = 0;

			// ID of offset to delete.
			const int OFFSET_TO_DELETE_ID = 0;

			// ID of offset to update.
			const int OFFSET_TO_UPDATE_ID = 0;

			// The document page number to retrieve hOCR data for.
			const int DOCUMENT_PAGE_NUMBER = 1;

			#endregion

			// This is needed because async main methods can't be assumed prior to VS2017/C#7.1
			Task.Run(async () =>
			{
				using (var offsetHelper = new OffsetApiHelper(HOSTNAME, USERNAME, PASSWORD, useHttps: true))
				using (var hOcrHelper = new HOcrApiHelper(HOSTNAME, USERNAME, PASSWORD, useHttps: true))
				{
					#region Create Offsets

					//// NOTE: The double double quotes and double curly braces in this string
					//// are needed to escape those characters in a C# verbatim interpolated string.
					////
					//// You likely will not need to escape them if you're making the request
					//// from a different language or tool.
					//var createRequestBody = $@"
					//{{
					//	""Offsets"": [
					//		{{
					//			""Id"": 0,
					//			""DocumentId"": {DOCUMENT_ID},
					//			""FieldId"": {FIELD_ID},
					//			""AssociatedArtifactId"": {DOCUMENT_ID},
					//			""ChoiceId"": null,
					//			""Offset"": 489,
					//			""Length"": 211,
					//			""Height"": null,
					//			""Width"": null,
					//			""Left"": null,
					//			""Top"": null,
					//			""PageNumber"": null
					//		}}
					//	]
					//}}";

					//Console.WriteLine("Making call to create/update offsets...");
					//var createOffsetResponseContent = await offsetHelper.UpsertOffsets(WORKSPACE_ID, DOCUMENT_ID, createRequestBody)
					//	.ConfigureAwait(false);
					//Console.WriteLine("Results:");
					//Console.WriteLine(createOffsetResponseContent);

					#endregion

					#region Delete Offset

					//Console.WriteLine("Making call to delete offset...");
					//await offsetHelper.DeleteOffset(WORKSPACE_ID, DOCUMENT_ID, OFFSET_TO_DELETE_ID);
					//Console.WriteLine("Request was successful.");

					#endregion

					#region Update Offsets

					//// NOTE: The double double quotes and double curly braces in this string
					//// are needed to escape those characters in a C# verbatim interpolated string.
					////
					//// You likely will not need to escape them if you're making the request
					//// from a different language or tool.
					//var updateRequestBody = $@"
					//{{
					//	""Offsets"": [
					//		{{
					//			""Id"": {OFFSET_TO_UPDATE_ID},
					//			""DocumentId"": {DOCUMENT_ID},
					//			""FieldId"": {FIELD_ID},
					//			""AssociatedArtifactId"": {DOCUMENT_ID},
					//			""ChoiceId"": null,
					//			""Offset"": 489,
					//			""Length"": 211,
					//			""Height"": null,
					//			""Width"": null,
					//			""Left"": null,
					//			""Top"": null,
					//			""PageNumber"": null
					//		}}
					//	]
					//}}";

					//Console.WriteLine("Making call to create/update offsets...");
					//var updateOffsetResponseContent = await offsetHelper.UpsertOffsets(WORKSPACE_ID, DOCUMENT_ID, updateRequestBody)
					//	.ConfigureAwait(false);
					//Console.WriteLine("Results:");
					//Console.WriteLine(updateOffsetResponseContent);

					#endregion

					#region Get Offsets

					//Console.WriteLine("Making call to get offsets...");
					//var getOffsetsResponseContent = await offsetHelper.GetOffsets(WORKSPACE_ID, DOCUMENT_ID).ConfigureAwait(false);
					//Console.WriteLine("Results:");
					//Console.WriteLine(getOffsetsResponseContent);

					#endregion

					#region Get hOCR Data

					//Console.WriteLine("Making call to get hOCR data...");
					//var getHOcrDataResponseContent = await hOcrHelper.GetHOcrData(WORKSPACE_ID, DOCUMENT_ID, DOCUMENT_PAGE_NUMBER);
					//Console.WriteLine("Results:");
					//Console.WriteLine(getHOcrDataResponseContent);

					#endregion
				}
			}).ConfigureAwait(false).GetAwaiter().GetResult();

		}
	}
}
