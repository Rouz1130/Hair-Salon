using  System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class Homemodule : NancyModule
  {
    public Homemodule()
    {
      Get["/"] = _ => {
        List<Stylist> allSytlists = Stylist.GetAll();
        return View["index.cshtml"];
      };

      Post["/stylist/new"] = _ => {
          Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
          newStylist.Save();
          List<Stylist> allStylists = Stylist.GetAll();
          return View["index.cshtml", allStylists];
        };
    }

  }
}
