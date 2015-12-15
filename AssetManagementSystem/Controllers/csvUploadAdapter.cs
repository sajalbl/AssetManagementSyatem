using AssetManagementSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AssetManagementSystem.Controllers
{
    //public class csvUploadAdapter
    //{
    //    public csvUploadResponse parseCSV(csvUploadRequest request)
    //    {
    //        csvUploadResponse response = new csvUploadResponse();

    //        List<dynamic> data = new List<dynamic>();
    //        data.Add(parse(request));
    //    }
    //}

//     public IEnumerable<dynamic> parse(csvUploadRequest request)
//        {
//            // TextFieldParser is in the Microsoft.VisualBasic.FileIO namespace.
//            using (TextFieldParser parser = new TextFieldParser(request.FilePath))
//            {
//                parser.CommentTokens = new string[] { "#" };
//                parser.SetDelimiters(new string[] { "," });
//                parser.HasFieldsEnclosedInQuotes = true;

//                // Skip over header line.
//                //parser.ReadLine();


//                string[] headerFields = parser.ReadFields();
//                string[] propertyNames = headerFields.Select(str => str.Replace(" ", "_")).ToArray<string>(); //(*)

//                while (!parser.EndOfData)
//                {
//                    string[] fields = parser.ReadFields();
//                    dynamic baseObject = new ExpandoObject();
//                    IDictionary<string, object> baseDataUnderlyingObject = baseObject;

//                    dynamic dataObject = new ExpandoObject();
//                    IDictionary<string, object> dataUnderlyingObject = dataObject;

//                    dynamic searchDataObject = new ExpandoObject();
//                    IDictionary<string, object> searchDataUnderlyingObject = searchDataObject;

//                    dynamic authData = new ExpandoObject();
//                    IDictionary<string, object> authDataUnderlyingObject = authData;

//                    dynamic authSearchDataObject = new ExpandoObject();
//                    IDictionary<string, object> authSearchDataUnderlyingObject = authSearchDataObject;

//                    for (int i = 0; i < headerFields.Length; i++)
//                    {
//                        var propName = propertyNames[i];
//                        var fieldValue = fields[i];


//                        var authField = (from a in request.AuthFieldNames
//                                         where a.ToUpper() == propName.ToUpper()
//                                         select a).FirstOrDefault();

//                        if (!string.IsNullOrEmpty(authField))
//                        {
//                            authDataUnderlyingObject.Add(propName, fieldValue);
//                            ExtractSearchData(request, authSearchDataUnderlyingObject, propName, fieldValue);
//                        }
//                        else
//                        {
//                            authDataUnderlyingObject.Add(propName, fieldValue);
//                            dataUnderlyingObject.Add(propName, fieldValue);
//                            ExtractSearchData(request, authSearchDataUnderlyingObject, propName, fieldValue);
//                            ExtractSearchData(request, searchDataUnderlyingObject, propName, fieldValue);
//                        }
//                    }
//                    baseDataUnderlyingObject.Add("Data", dataObject);
//                    baseDataUnderlyingObject.Add("SearchData", searchDataObject);
//                    baseDataUnderlyingObject.Add("AuthData", authData);
//                    baseDataUnderlyingObject.Add("AuthSearchData", authSearchDataObject);
//                    yield return baseObject;
//                }
//            }
//        }

//        private static void ExtractSearchData(UpdateListSearchRequest request, IDictionary<string, object> searchDataObject, string propName, string fieldValue)
//        {
//            var searchField = (from a in request.SearchFieldNames
//                               where a.ToUpper() == propName.ToUpper()
//                               select a).FirstOrDefault();
//            if (!string.IsNullOrEmpty(searchField))
//            {
//                searchDataObject.Add(propName, fieldValue);
//            }
//        }

}