using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Cadru.Net.Rest;
using Cadru.Net.Http;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace Cadru.Core.UnitTests.Net.Rest
{
    public class TestClient : RestClient
    {
        public TestClient() : base(new CompressingHttpClientHandler()) { }

        [JsonRoute("api/districts/lookup/{value}", "districts.nutrislice.com")]
        public async Task<List<SchoolDistrict>> DistrictLookup(string value)
        {
//            var request = this.CreateHttpRequestMessageForRoute(new RouteValueDictionary { ["value"] = value, ["bar"] = "foobar" });
            return await this.SendAsync<List<SchoolDistrict>>(new { value = value, bar = "foobar" });
        }

        [JsonRoute("/district/{district_url}", "districts.nutrislice.com", Method = RestMethod.Get)]
        public async Task<SchoolDistrict> SetDistrict(SchoolDistrict district)
        {
            var districtUrl = new Uri(district.district_url);
            var request = this.CreateHttpRequestMessageForRoute(new RouteValueDictionary { ["district_url"] = districtUrl.Host });
            return await this.SendAsync<SchoolDistrict>(request, CancellationToken.None);
        }

        [JsonRoute("/menu/api/settings")]
        public async Task<Settings> GetDistrictSettings(SchoolDistrict district)
        {
            var request = this.CreateHttpRequestMessageForRoute(district.district_url);
            return await this.SendAsync<Settings>(request, CancellationToken.None);
        }
    }

    public class SchoolDistrict
    {
        public string name { get; set; }
        public int id { get; set; }
        public string state { get; set; }
        public string district_url { get; set; }
        public bool requires_id_for_student_users { get; set; }
    }

    public class Settings
    {
        public string state { get; set; }
        public bool food_rating_enabled { get; set; }
        public string location_selector_text { get; set; }
        public string mobile_home_page_message { get; set; }
        public string department_name { get; set; }
        public string mobile_button_text { get; set; }
        public string first_line_of_eula { get; set; }
        public bool bold_all_entrees_enabled { get; set; }
        public bool allergy_filter_enabled { get; set; }
        public string hash { get; set; }
        public string district_name { get; set; }
        public int latest_eula_revision { get; set; }
        public int mymealplan_enabled { get; set; }
        public string mobile_logo_img_url { get; set; }
        public object menus_availability_limit { get; set; }
        public bool mobile_user_profile_enabled { get; set; }
        public string eula_url { get; set; }
        public string logo_img_url { get; set; }
        public string announcement_list_img_url { get; set; }
        public bool mobile_menus_disabled { get; set; }
        public string mobile_button_url { get; set; }
    }

    [TestClass, ExcludeFromCodeCoverage]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
//using (var w = new HttpClient())
//{
//    var a = await w.GetAsync("http://districts.nutrislice.com/api/districts/lookup/hill");
//    w.Timeout = TimeSpan.FromMinutes(2);
//    var b = await w.GetAsync("http://districts.nutrislice.com/api/districts/lookup/hill");
//}

            using (var c = new TestClient())
            {
                var districts = await c.DistrictLookup("hill");
                var district = await c.SetDistrict(districts[3]);
                var settings = await c.GetDistrictSettings(districts[3]);
            }
        }
    }
}
