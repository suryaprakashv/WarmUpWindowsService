using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers; 
using IdentityModel.Client;

using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace WarmUpWindowsService
{
    public partial class ClientPortalWarmUpService : ServiceBase
    {
        private static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string roClientId = ConfigurationManager.AppSettings["ResourceOwnerClientId"];
        private static string roClientSecret = ConfigurationManager.AppSettings["ResourceOwnerClientSecret"];
        private static string requestScope = ConfigurationManager.AppSettings["RequestScope"];
        private static string tokenEndPoint = ConfigurationManager.AppSettings["IamTokenEndpoint"];
        private static string saName = ConfigurationManager.AppSettings["SpServiceAccountName"];
        private static string saPassword = ConfigurationManager.AppSettings["SpServiceAccountPassword"];
        private static string subSiteUrl = ConfigurationManager.AppSettings["SubSiteUrl"];
        private static string documentLibraryGuid = ConfigurationManager.AppSettings["DocumentLibraryGuid"];
        private static string apiUri = ConfigurationManager.AppSettings["ApiUri"];
        Timer timer = new Timer();
        public ClientPortalWarmUpService()
        {
            InitializeComponent();
            timer.Interval = 20000;
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(APIWarmup_Tick);
        }

        protected override void OnStart(string[] args)
        {
            logger.Info("Windows service started."); 
           
        }

        protected override void OnStop()
        {
        }

        private async void APIWarmup_Tick(object sender, EventArgs e)
        {
            timer.Interval = 3600000;
            logger.Info("API Warm Up Strated.");
            try
            { 

                var client = new TokenClient(tokenEndPoint, roClientId, roClientSecret);
                var response = client.RequestResourceOwnerPasswordAsync(saName, saPassword, requestScope).Result;
                if (response.AccessToken != null)
                {
                    logger.Info("AccessToken:"+response.AccessToken);
                    var documentResponse =
                        await GetLibraryContent(subSiteUrl, new Guid(documentLibraryGuid), response.AccessToken);

                    logger.Info("API Warm Up Completed successfully.");
                }
                else
                {
                    logger.Info("API Warm Up failed: unable to fetch access token. "+response.ErrorDescription);
                }
            }
            catch (Exception ex)
            {
                logger.Info("API Warm Up failed with error: "+ex.Message);
                logger.Info("API Warm Up failed with error: " + ex.StackTrace);
            }
        }

        private async Task<ResultDto<DocumentItemListDto>> GetLibraryContent(string subSiteUrl, Guid documentLibraryGuid,string accessToken)
        {
            

            ApiManager _apiManager = new ApiManager(); 
              return
                await
                    _apiManager.GetAsync<ResultDto<DocumentItemListDto>>(
                        $"{apiUri}/Document?subSiteUrl={subSiteUrl}&documentLibraryGuid={documentLibraryGuid}", accessToken);
        }

    }
}
