using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFTestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SuperHeroService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SuperHeroService.svc or SuperHeroService.svc.cs at the Solution Explorer and start debugging.
    public class SuperHeroService : ISuperHeroService
    {
        public SuperHero AddHero(SuperHero hero)
        {
            hero.Id = Data.SuperHeroes.Max(sh => sh.Id) + 1;
            Data.SuperHeroes.Add(hero);
            return hero;
        }

        public List<SuperHero> DeleteHero(string id)
        {
            Data.SuperHeroes = Data.SuperHeroes.Where(sh => sh.Id != int.Parse(id)).ToList();
            return Data.SuperHeroes;
        }

        public string DoWork()
        {
            return "This is the SuperHeroDB service!";
        }

        public string Fight(string id1, string id2)
        {
            SuperHero hero1 = Data.SuperHeroes.Find(hero => hero.Id == int.Parse(id1));
            SuperHero hero2 = Data.SuperHeroes.Find(hero => hero.Id == int.Parse(id2));

            if (hero1.Combat > hero2.Combat)
                return $"{hero1.HeroName} wins!";
            else if (hero2.Combat > hero1.Combat)
                return $"{hero2.HeroName} wins!";
            else
                return $"It's a tie!";
        }

        public List<SuperHero> GetAllSuperHeroes()
        {
            return Data.SuperHeroes;
        }

        public SuperHero GetHero(string id)
        {
            return Data.SuperHeroes.Find(sh => sh.Id == int.Parse(id));
        }

        public List<SuperHero> GetSortedHeroList(string type)
        {
            switch (type)
            {
                case "firstname":
                    return Data.SuperHeroes.OrderBy(hero => hero.FirstName).ThenBy(hero => hero.LastName).ToList();
                case "lastname":
                    return Data.SuperHeroes.OrderBy(hero => hero.LastName).ThenBy(hero => hero.FirstName).ToList();
                case "hero":
                    return Data.SuperHeroes.OrderBy(hero => hero.HeroName).ThenBy(hero => hero.FirstName).ToList();
                case "birthplace":
                    return Data.SuperHeroes.OrderBy(hero => hero.PlaceOfBirth).ThenBy(hero => hero.FirstName).ToList();
                case "combat":
                    return Data.SuperHeroes.OrderBy(hero => hero.Combat).ThenBy(hero => hero.FirstName).ToList();
                default:
                    return Data.SuperHeroes.OrderBy(hero => hero.FirstName).ThenBy(hero => hero.LastName).ToList();
            }
        }

        public List<SuperHero> SearchHero(string searchText)
        {
            List<SuperHero> result = Data.SuperHeroes.Where<SuperHero>(
                sh => sh.FirstName.ToLower().Contains(searchText) ||
                sh.LastName.ToLower().Contains(searchText) ||
                sh.HeroName.ToLower().Contains(searchText) ||
                sh.PlaceOfBirth.ToLower().Contains(searchText)).ToList<SuperHero>();

            if (result.Count == 0)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                WebOperationContext.Current.OutgoingResponse.StatusDescription = "No Hero Found!";
                //throw new WebFaultException<string>("No Hero Found!", System.Net.HttpStatusCode.NotFound);
            }
                
            return result;
        }

        public SuperHero UpdateHero(SuperHero updatedHero, string id)
        {
            SuperHero hero = Data.SuperHeroes.Where(sh => sh.Id == int.Parse(id)).FirstOrDefault();
            hero.FirstName = updatedHero.FirstName;
            hero.LastName = updatedHero.LastName;
            hero.HeroName = updatedHero.HeroName;
            hero.PlaceOfBirth = updatedHero.PlaceOfBirth;
            hero.Combat = updatedHero.Combat;

            return hero;
        }
    }
}
