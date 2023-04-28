using MvcCoreApiCrudPersonaje.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCoreApiCrudPersonaje.Services
{
    public class ServiceApiPersonaje
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlAPi;

        public ServiceApiPersonaje(IConfiguration configuration)
        {
            this.Header=
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlAPi=configuration.GetValue<string>
                ("ApiUrls:ApiCrudHospitales");
        }

        public async Task<T> CallApiASync<T>(string request)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlAPi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response=
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesASync()
        {
            string request = "/api/personajes";
            List<Personaje> personajes=
                await this.CallApiASync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje> FindPersonajeAsync(int idperso)
        {
            string request = "/api/personajes/" + idperso;
            Personaje personaje =
                await this.CallApiASync<Personaje>(request);
            return personaje;
        }

        public async Task DeletePersonajeASync(int idperso)
        {
            using(HttpClient client= new HttpClient())
            {
                string request = "/api/personajes/" + idperso;
                client.BaseAddress = new Uri(this.UrlAPi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }

        public async Task InsertPersonajesAsync(string nombre, string imagen, int idserie)
        {
            using(HttpClient client=new HttpClient())
            {
                string request = "/api/personajes";
                client.BaseAddress=new Uri(this.UrlAPi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje per = new Personaje();
                per.NombrePersonaje = nombre;
                per.Imagen = imagen;
                per.IdSerie = idserie;
                string json=JsonConvert.SerializeObject(per);
                StringContent content=
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response=
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePErsonajeASync(int idperso, string nombre, string imagen, int idserie)
        {
            using(HttpClient client=new HttpClient())
            {
                string request = "/api/personajes";
                client.BaseAddress = new Uri(this.UrlAPi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje per =
                    new Personaje
                    {
                        IdPersonaje = idperso,
                        NombrePersonaje = nombre,
                        Imagen = imagen,
                        IdSerie = idserie
                    };
                string json = JsonConvert.SerializeObject(per);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }
    }
}
