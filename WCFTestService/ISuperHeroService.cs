using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFTestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISuperHeroService" in both code and config file together.
    [ServiceContract]
    public interface ISuperHeroService
    {
        //[OperationContract]
        [OperationContract, WebGet(ResponseFormat = WebMessageFormat.Json)]
        string DoWork();

        //[OperationContract, WebGet(ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetAllSuperHeroes")]
        List<SuperHero> GetAllSuperHeroes();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHero/{id}")]
        SuperHero GetHero(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddHero")]
        SuperHero AddHero(SuperHero hero);

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateHero/{id}")]
        SuperHero UpdateHero(SuperHero updatedHero, string id);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteHero/{id}")]
        List<SuperHero> DeleteHero(string id);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "SearchHero/{searchText}")]
        List<SuperHero> SearchHero(string searchText);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSortedHeroList/{type}")]
        List<SuperHero> GetSortedHeroList(string type);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Fight/{id1}/{id2}")]
        string Fight(string id1, string id2);
    }
}
