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
      Get["/clients"] = _ => {
        List<Client> AllClients = Client.GetAll();
        return View["clients.cshtml", AllClients];
      };

      Get["/clients/new"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["clients_form.cshtml", AllStylists];
      };

      Post["/clients/delete"] = _ => {
        Client.DeleteAll();
       return View["cleared.cshtml"];
       };

       Get["/stylists"] = _ => {
         List<Stylist> allSytlists = Stylist.GetAll();
         return View["stylists.cshtml", allSytlists];
       };


       Get["/stylists/new"] = _ => {
         return View["stylists_from.cshtml"];
       };


       Get["stylist/edit/{id}"] = parameters => {
         Stylist SelectedStylist = Stylist.Find(parameters.id);
         return View["stylist_edit.cshtml", SelectedStylist];
       };

    }

  }
}
