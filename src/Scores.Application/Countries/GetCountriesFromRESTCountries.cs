using Scores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Scores.Application.Countries
{
    public static class GetCountriesFromRESTCountries
    {
        public static async Task<IEnumerable<Country>> Do()
        {
            string url = "https://restcountries.eu/rest/v2/all";

            using HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var countries = await response.Content.ReadAsAsync<IEnumerable<Country>>();

                return countries;
            }

            throw new Exception(response.ReasonPhrase);
        }
    }
}
