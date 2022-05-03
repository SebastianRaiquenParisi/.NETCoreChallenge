using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vehiculosCRUD.Models;
using vehiculosCRUD.SourceGeneratedJSON;

namespace vehiculosCRUD.Controllers
{
    public class PropietarioController : Controller
    {
        private async Task<List<Propietario>> getPropietariosJSON3PrimerasPaginas(int i)
        {
            List<Propietario> propietarios = new List<Propietario>();
            propietarios = await getPropietariosJSONPaginaNumero(1, propietarios);
            propietarios = await getPropietariosJSONPaginaNumero(2, propietarios);
            propietarios = await getPropietariosJSONPaginaNumero(3, propietarios);
            return propietarios;
        }
        
        private async Task<List<Propietario>> getPropietariosJSONPaginaNumero(int i, List<Propietario> propietarios)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://reqres.in/api/users?page=" + i.ToString());
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            propietarios = await agregarPropietariosJSON(responseBody, propietarios);
            return propietarios;
        }
        private async Task<List<Propietario>> agregarPropietariosJSON(string json, List<Propietario> propietarios)
        {

            return propietarios;
        }



        //findPropietario(id)
        //
        //
        //
        //
        //
        //}
        //
        //
        //subirPaginaNumero(num){
        //  url += num;
        //  url into json
        //  decryptPropietarios(JSON) => propietarios
        //      foreach(propietario : propietarios){
        //           subirPropietarioDatabase()
        //      }
        //  }
        //  
        //}
        //
        //
        //subir3PrimerasPaginasBD(){
        //  subirPaginaNumero(1)
        //  subirPaginaNumero(2)
        //  subirPaginaNumero(3)
        //}


    }
}
