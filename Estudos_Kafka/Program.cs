using System.Threading.Tasks;

namespace Estudos_Kafka
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await FraudDetectorService.ConsumirMensagemDeTopico();
            //await FraudDetectorService.PublicarMensagemEmTopico();
        }
    }
}
