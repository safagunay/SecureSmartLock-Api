using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;

namespace LockerApi.Services
{
    public class EthClient
    {
        private static Web3 _web3;
        private static string _contractAddress;
        private static Account _account;


        public EthClient()
        {
            var url = "http://23.101.70.73:8545";
            var privateKey = "0xad0a6ff11daefd02ee3276fce1e62d787d24a30e1ee0aa471e653d5667044462";
            _account = new Account(privateKey);
            _web3 = new Web3(_account, url);
        }

        public async Task<bool> deployContract()
        {
            var deploymentMessage = new DeploymentMessage();
            var deploymentHandler = _web3.Eth.GetContractDeploymentHandler<DeploymentMessage>();
            var transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
            _contractAddress = transactionReceipt.ContractAddress;
            return _contractAddress != null;
        }

        //public async void addDevice(uint devId)
        //{
        //    var addDevFuncMessage = new AddDeviceFunctionMessage()
        //    {
        //        DevId = devId
        //    };
        //    var handler = _web3.Eth.GetContractQueryHandler<AddDeviceFunctionMessage>();
        //    await handler.QueryRawAsync(_contractAddress, addDevFuncMessage);
        //}

        public async Task<string> addLogToDevice(uint devId, string log)
        {
            var addLogToDeviceFunctionMessage = new AddLogToDeviceFunctionMessage()
            {
                DevId = devId,
                Log = log
            };
            var handler = _web3.Eth.GetContractQueryHandler<AddLogToDeviceFunctionMessage>();
            return await handler.QueryRawAsync(_contractAddress, addLogToDeviceFunctionMessage);

        }

        public async Task<string> getLogs(uint devId)
        {
            var getLogsFunctionMessage = new GetLogsFunctionMessage()
            {
                DevId = devId
            };
            var handler = _web3.Eth.GetContractQueryHandler<GetLogsFunctionMessage>();
            return await handler.QueryAsync<string>(_contractAddress, getLogsFunctionMessage);
        }
    }

    
}