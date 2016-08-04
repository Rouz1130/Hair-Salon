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
    }

  }
}
