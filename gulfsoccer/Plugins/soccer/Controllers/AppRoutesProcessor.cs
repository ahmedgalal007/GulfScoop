// /Tournament/year/season/Week/Match/Team/Formation/Player
// Tournament_year_season_Week_Match_Team_Formation_PlayerController
// example  /Egyptian-Leage/year-2019/s1/w2/Alahly-Zamalek/Alahly/formation/mohamed-elshenawy
// /Tournament/
//            /year:string(year-1945)
//                 /season:string(S1|S2)
//                               /Week:String(W1|w2|w3...)
//                                           /Match(TeamhomeName-TeamAwayName)String
//                                                  /TeamFormation
//                                                                /Player(MatchStates)
// /Tournament/club/team|trophy|match
// example  /Egyptian-Leage/Alahly/team|trophy|match
//            /club:String
//                 /Team:string
//                 /Trophy:string
//                 /Match:string
// /club:string
//      /Tournaments
//      /Teams
// /player:string
//        /clubs
//        /matches
//        /cards
//        /injuries
//        /goals

using gulfsoccer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace gulfsoccer.Plugins.soccer.Controllers
{
    public static partial class AppRoutesProcessor
    {
        public static ControllerRedirectContext GetRoute(string url, ApplicationDbContext Db)
        {
            if (!String.IsNullOrEmpty(url))
            {
                string prefix = "../soccer/"; 
                string[] Params = url.TrimStart('/').TrimEnd('/').Split('/');
                ControllerRedirectContext result = new ControllerRedirectContext();
                if (Params.Length > 0)
                {
                    // if in Tournaments
                    if (Settings.Tournaments.Select(T => T.Name).Contains(Params[0]))
                    {
                        result.RouteValues.Add("tournament", Settings.Tournaments.Where(T => T.Name == Params[0]).Select(T => T.Name).FirstOrDefault());
                        result.ControllerName = "Tournament";
                        if (Params.Length > 1 && Settings.Clubs.Select(C => C.Name).Contains(Params[1]))
                        {
                            result.ControllerName += "_club";
                            result.RouteValues.Add("club", Params[1]);
                            if (Params.Length > 2)
                            {
                                result.ControllerName += "_" + Params[2];
                                result.RouteValues.Add("club", Params[2]);
                            }
                        }
                        else
                            tournamentParamProcess(Params, ref result);

                        result.ViewName = prefix + result.ControllerName;
                    }
                    // if in clubs
                    else if (Settings.Clubs.Select(T => T.Name).Contains(Params[0]))
                    {
                        result.RouteValues.Add("club", Settings.Tournaments.Where(T => T.Name == Params[0]).Select(T => T.Name).FirstOrDefault());
                        result.ControllerName = "Club";
                        clubParamProcess(Params, ref result);
                    }
                    // if in players
                    else if (Settings.Players.Select(T => T.Name).Contains(Params[0]))
                    {
                        result.RouteValues.Add("player", Settings.Tournaments.Where(T => T.Name == Params[0]).Select(T => T.Name).FirstOrDefault());
                        result.ControllerName = "Player";
                        playerParamProcess(Params, ref result);
                    }
                    else {
                        // Process the default route.
                        string[] path = new string[] { "Controller", "Action", "Id"};
                        string[] defaults = new string[] { "Home", "Index", null };
                        result.ControllerName = Params.Length>0?Params[0]:"Home";
                        for (int i = 0; i < 3; i++)
                        {
                            if (Params.Length > i)
                            {
                                result.RouteValues.Add(path[i].ToLower(), Params[i]);
                            }
                            else
                            {
                                result.RouteValues.Add(path[i].ToLower(), defaults[i]);
                            }
                            
                        }

                    }
                }
                return result;
            }
            else
            {
                return new ControllerRedirectContext { ControllerName = "Home", ViewName = "Index" };
            }
            
        }

        public static void tournamentParamProcess(string[] arr ,ref ControllerRedirectContext CRC)
        {
            int outNumber;
            string[] path = new string[] { "Tournament", "Year", "Season", "Week", "Match", "Team", "Formation", "Player" };
            for (int i = 1; i < arr.Length; i++)
            {
                if (int.TryParse(arr[i], out outNumber))
                {
                    paramProcessAsDate(i,arr,ref CRC);
                    break;
                }
                else
                {
                    CRC.ControllerName += "_" + path[i];
                    CRC.RouteValues.Add(path[i].ToLower(), arr[i]);
                }
            }

        }
        public static void clubParamProcess(string[] arr, ref ControllerRedirectContext CRC)
        { }
        public static void playerParamProcess(string[] arr, ref ControllerRedirectContext CRC) 
        { }
        public static void paramProcessAsDate(int startIndex, string[] arr,ref ControllerRedirectContext CRC)
        {
            int outNumber;
            string[] path = new string[] { "year", "month", "day" };
            CRC.ControllerName += "_date";
            if (arr.Length > startIndex + 3)
            {
                paramProcess404(ref CRC);
                return;
            }
            for (int i = startIndex; i < arr.Length; i++)
            {
                
                if (int.TryParse(arr[i], out outNumber))
                {
                    CRC.RouteValues.Add(path[i], arr[i]);
                }
                else
                {
                    break;
                }
            }
        }

        public static void paramProcess404(ref ControllerRedirectContext CRC)
        {
            CRC.ControllerName = "404";
            CRC.RouteValues = null;
        }
    }

    public class ControllerRedirectContext
    {
        public ControllerRedirectContext(){
            this.RouteValues = new RouteValueDictionary();        
        }
        public string ControllerName { get; set; }
        public string ViewName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
    }
}