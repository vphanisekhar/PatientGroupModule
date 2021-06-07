using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> PatientGroups([FromBody]dynamic jsonRequest)
        {
            int count = 0;
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonRequest.ToString());

            int[][] arrays = myDeserializedClass.matrix.Select(a => a.ToArray()).ToArray();

            for (int i = 0; i < arrays.Length; i++)
            {
                for (int j = 0; j < arrays[i].Length-1; j++)
                {
                    if (j < arrays[i].Length-1)
                        if((arrays[i][j] == arrays[i][j + 1]) && (arrays[i][j] ==1 && arrays[i][j + 1] ==1))
                        count++;

                }
                
            }

            int noOfColumns = arrays[0].Length;
            for (int j = 0; j <= noOfColumns-1; j++)
            {
                for (int i = 0; i < arrays.Length; i++)
                {
                    if (i < arrays.Length - 1)
                        if ((arrays[i][j] == arrays[i + 1][j]) && (arrays[i][j] ==1 && arrays[i + 1][j] ==1))
                        count++;
                }
                
            }

            
            int obliqueCount= Oblique(arrays, arrays.Length, noOfColumns);
            count = obliqueCount + count;
                       
            return Ok(count);
        }


        static int Oblique(int[][] matrix, int rows, int cols)
        {
            int count = 0;
            bool indicator = false;
           
            for (int k = 0; k <= rows - 3; k++)
            {
                for (int l = 0; l <= cols - 3; l++)
                {
                    if ((k <= rows - 3) && l <= cols - 3)
                    {
                        if ((matrix[k][l] == matrix[k + 1][l + 1]) && (matrix[k][l] == 1 && matrix[k + 1][l + 1] == 1))
                            indicator = true;
                        if ((indicator = true) && ((matrix[k][l] == matrix[k + 1][l + 1]) && matrix[k][l] == matrix[k + 2][l + 2] ) && (matrix[k][l] == 1 && matrix[k + 1][l + 1] == 1))
                        {
                            indicator = true;
                            count++;
                        }
                        else
                        {
                            indicator = false;
                        }
                    }
                }
            }
            return count;
        }

                // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Root
        {
            public List<List<int>> matrix { get; set; }
        }

    }
}