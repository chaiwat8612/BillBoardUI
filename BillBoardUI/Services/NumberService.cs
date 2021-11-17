using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using BillBoardUI.Models;
using BillBoardUI.Services.Configure;

namespace BillBoardUI.Services
{
    public class NumberService
    {
        private readonly string _urlApi = "";
        readonly private string _statusSuccess = "200";
        readonly CallApiService _callApiService;
        readonly ConvertDataService _convertDataService;

        ConfigureService _configureService = new ConfigureService();
        IConfiguration _getConfig;

        public NumberService()
        {
            _callApiService = new CallApiService();
            _convertDataService = new ConvertDataService();

            _getConfig = _configureService.GetConfigFromAppsetting();
            _urlApi = _getConfig["URLAPI:URL_BILLBOARD_API"].ToString();
        }

        internal DataSet GetNumberListHomepage(ref string messageErrorFromService)
        {
            try
            {
                string url = _urlApi + "Number/GetNumberListHomepage";
                var resultFromAPI = this._callApiService.Get(url);

                ResultModel objGet = JsonConvert.DeserializeObject<ResultModel>(resultFromAPI.Result);
                string statusObjectGet = objGet.status.ToString();
                if(statusObjectGet == _statusSuccess)
                {
                    List<NumberModel> objGetData = JsonConvert.DeserializeObject<List<NumberModel>>(objGet.data.ToString());

                    return this._convertDataService.ConvertListToDataSet(objGetData);
                }
                else
                {
                    messageErrorFromService += " Not Found GetNumberListHomepage;";
                    return null;
                }
            }
            catch
            {
                messageErrorFromService += " Not Connection API Number/GetNumberListHomepage;";
                return null;
            }
        }

        public string SaveNewNumber(SaveNewNumberModel saveNewNumberModel, ref string messageError)
        {
            string url = _urlApi + "Number/SaveNewNumber";

            try
            {
                var resultFromAPI = this._callApiService.Post(url, saveNewNumberModel);
                ResultModel objGet = JsonConvert.DeserializeObject<ResultModel>(resultFromAPI.Result);
                string statusObjectGet = objGet.status.ToString();
                string idNewNumber = objGet.data.ToString();

                if (statusObjectGet == _statusSuccess)
                {
                    return "200";
                }
                else
                {
                    messageError += "Submit data fail";
                    return "502";
                }
            }
            catch
            {
                messageError += "Submit data fail server;";
                return "503";
            }
        }
    }
}

