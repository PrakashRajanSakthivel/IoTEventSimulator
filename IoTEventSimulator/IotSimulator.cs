using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Configuration;
using MySqlConnector;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;

namespace IoTEventSimulator
{
    public static class IotSimulator
    {
        [FunctionName("IoTEventSimulator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var requestBody = string.Empty;
            var fleet_id = string.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }

            var simulatorDetails = JsonConvert.DeserializeObject<SimulatorDetails>(requestBody);
            try
            {
                if (simulatorDetails.SimulatorIdentifier == Constants.Supplier)
                {
                    fleet_id = Helpers.GetFleetId(simulatorDetails, Constants.RawMaterial, Constants.Fleet_ID);
                    if (string.IsNullOrWhiteSpace(fleet_id))
                    {
                        return new BadRequestObjectResult("failed to fetch the details from database, please validate batch id");
                    }
                    Helpers.AddSimulatedData(Constants.supplierFleetLocations, fleet_id, simulatorDetails.BatchId);

                }
                else if (simulatorDetails.SimulatorIdentifier == Constants.Manufacturer)
                {
                    fleet_id = Helpers.GetFleetId(simulatorDetails, Constants.ProductPickup, Constants.FleetID);
                    if (string.IsNullOrWhiteSpace(fleet_id))
                    {
                        return new BadRequestObjectResult("failed to fetch the details from database, please validate batch id");
                    }
                    Helpers.AddSimulatedData(Constants.manufacturerFleetLocation, fleet_id, simulatorDetails.BatchId);
                }
                else
                {
                    return new BadRequestObjectResult("invalid workflow value. allowed values for identifier are supplier, manufacturer");

                }
            }
            catch
            {
                return new BadRequestObjectResult("failed to fetch the details from database");

            }

            return new OkObjectResult("success");
        }


    }
}
