using BASRON.Serverless.Model;
using BASRON.Service;
using INVCOM.ServerlessTest.Models;
using INVCOM.ServerlessTest.TestData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;

namespace INVCOM.ServerlessTest.Controllers
{
    public class GraphQLControllerTests : XunitMemberDataInput, IClassFixture<TestFixture>
    {
        private GraphQLController _graphQLController;
        private readonly TestFixture _fixture;

        /// <summary>
        /// Constuctor GraphQLControllerTests
        /// </summary>
        public GraphQLControllerTests(TestFixture testFixture)
        {
            _fixture = testFixture;
            _graphQLController = _fixture._graphQLController;

        }
        [Theory]
        [MemberData(nameof(GraphQLModelData))]
        public async Task Get_Transactions_Valid_Query(GraphQLModel graphQLModel, CancellationToken cancellationToken = default)
        {
            var controllerResult = await _graphQLController.HandleRequest(graphQLModel, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(200, ((ObjectResult)controllerResult).StatusCode);
            var result = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value)));
            Assert.NotNull(result);
            Assert.True(result.bTransactions.Any() || result.bTransactions.Count() == 0);
        }
        [Theory]
        [MemberData(nameof(GraphQLModelDataForCreate))]
        public async Task Save_New_Transaction_With_Valid_Query(GraphQLModel GetTransaction, GraphQLModel AddTransaction, CancellationToken cancellationToken = default)
        {
            var getControllerResult = await _graphQLController.HandleRequest(GetTransaction, cancellationToken);
            Assert.NotNull(getControllerResult);
            Assert.Equal(200, ((ObjectResult)getControllerResult).StatusCode);
            var getTransactionResult = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerResult).Value)));
            Assert.NotNull(getTransactionResult);
            int TransactionCountBeforeAddingNewTransaction = getTransactionResult.bTransactions.Count();

            var addcontrollerResult = await _graphQLController.HandleRequest(AddTransaction, cancellationToken);
            Assert.NotNull(addcontrollerResult);
            Assert.Equal(200, ((ObjectResult)addcontrollerResult).StatusCode);
            var addTransactionResult = JsonConvert.DeserializeObject<CreateTransactionResponse>(JsonConvert.SerializeObject((((ObjectResult)addcontrollerResult).Value)));
            Assert.NotNull(addTransactionResult);
            Assert.NotNull(addTransactionResult.createBTransaction);
            Assert.NotNull(addTransactionResult.createBTransaction.referenceNumber);

            var getControllerResultAfterSave = await _graphQLController.HandleRequest(GetTransaction, cancellationToken);
            Assert.NotNull(getControllerResultAfterSave);
            Assert.Equal(200, ((ObjectResult)getControllerResultAfterSave).StatusCode);
            var getTransactionResultAfterSave = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerResultAfterSave).Value)));
            Assert.NotNull(getTransactionResultAfterSave);
            int TransactionCountafterAddingNewTransaction = getTransactionResultAfterSave.bTransactions.Count();

            Assert.True(TransactionCountafterAddingNewTransaction > TransactionCountBeforeAddingNewTransaction);
            Assert.Equal(TransactionCountafterAddingNewTransaction, TransactionCountBeforeAddingNewTransaction + 1);


        }

        [Theory]
        [MemberData(nameof(GraphQLModelDataForDelete))]
        public async Task Delete_Transaction_With_Valid_Query(GraphQLModel GetTransaction, GraphQLModel AddTransaction, GraphQLModel DeleteTransaction, CancellationToken cancellationToken = default)
        {
            var getControllerResult = await _graphQLController.HandleRequest(GetTransaction, cancellationToken);
            Assert.NotNull(getControllerResult);
            Assert.Equal(200, ((ObjectResult)getControllerResult).StatusCode);
            var getTransactionResult = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerResult).Value)));
            Assert.NotNull(getTransactionResult);
            int TransactionCountBeforeAddingNewTransaction = getTransactionResult.bTransactions.Count();

            var addcontrollerResult = await _graphQLController.HandleRequest(AddTransaction, cancellationToken);
            Assert.NotNull(addcontrollerResult);
            Assert.Equal(((ObjectResult)addcontrollerResult).StatusCode, 200);
            var addTransactionResult = JsonConvert.DeserializeObject<CreateTransactionResponse>(JsonConvert.SerializeObject((((ObjectResult)addcontrollerResult).Value)));
            Assert.NotNull(addTransactionResult);
            Assert.NotNull(addTransactionResult.createBTransaction);
            Assert.NotNull(addTransactionResult.createBTransaction.referenceNumber);

            var getControllerResultAfterSave = await _graphQLController.HandleRequest(GetTransaction, cancellationToken);
            Assert.NotNull(getControllerResultAfterSave);
            Assert.Equal(200, ((ObjectResult)getControllerResultAfterSave).StatusCode);
            var getTransactionResultAfterSave = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerResultAfterSave).Value)));
            Assert.NotNull(getTransactionResultAfterSave);
            int TransactionCountafterAddingNewTransaction = getTransactionResultAfterSave.bTransactions.Count();

            Assert.True(TransactionCountafterAddingNewTransaction > TransactionCountBeforeAddingNewTransaction);
            Assert.Equal(TransactionCountafterAddingNewTransaction, TransactionCountBeforeAddingNewTransaction + 1);

            DeleteTransaction.Variables["btransactionId"] = addTransactionResult.createBTransaction.referenceNumber;

            var controllerResult = await _graphQLController.HandleRequest(DeleteTransaction, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(200, ((ObjectResult)controllerResult).StatusCode);
            var deleteTransactionResult = JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value));
            Assert.NotNull(deleteTransactionResult);
            Assert.Contains("successfully deleted from db", deleteTransactionResult);

            var getControllerAfterDeleteResult = await _graphQLController.HandleRequest(GetTransaction, cancellationToken);
            Assert.NotNull(getControllerAfterDeleteResult);
            Assert.Equal(200, ((ObjectResult)getControllerAfterDeleteResult).StatusCode);
            var getTransactionAfterDeleteResult = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerAfterDeleteResult).Value)));
            Assert.NotNull(getTransactionAfterDeleteResult);
            int TransactionCountAfterDelete = getTransactionAfterDeleteResult.bTransactions.Count();
            Assert.Equal(TransactionCountAfterDelete, TransactionCountBeforeAddingNewTransaction);
        }

        [Theory]
        [MemberData(nameof(GraphQLModelDataForUpdate))]
        public async Task Update_Transaction_With_Valid_Query(GraphQLModel GetTransaction, GraphQLModel AddTransaction, GraphQLModel UpdateTransaction, GraphQLModel DeleteTransaction, CancellationToken cancellationToken = default)
        {
            var getControllerResult = await _graphQLController.HandleRequest(GetTransaction, cancellationToken);
            Assert.NotNull(getControllerResult);
            Assert.Equal(200, ((ObjectResult)getControllerResult).StatusCode);
            var getTransactionResult = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerResult).Value)));
            Assert.NotNull(getTransactionResult);
            int TransactionCountBeforeAddingNewTransaction = getTransactionResult.bTransactions.Count();

            var addcontrollerResult = await _graphQLController.HandleRequest(AddTransaction, cancellationToken);
            Assert.NotNull(addcontrollerResult);
            Assert.Equal(200, ((ObjectResult)addcontrollerResult).StatusCode);
            var addTransactionResult = JsonConvert.DeserializeObject<CreateTransactionResponse>(JsonConvert.SerializeObject((((ObjectResult)addcontrollerResult).Value)));
            Assert.NotNull(addTransactionResult);
            Assert.NotNull(addTransactionResult.createBTransaction);
            Assert.NotNull(addTransactionResult.createBTransaction.referenceNumber);

            var getControllerResultAfterSave = await _graphQLController.HandleRequest(GetTransaction, cancellationToken);
            Assert.NotNull(getControllerResultAfterSave);
            Assert.Equal(200, ((ObjectResult)getControllerResultAfterSave).StatusCode);
            var getTransactionResultAfterSave = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerResultAfterSave).Value)));
            Assert.NotNull(getTransactionResultAfterSave);
            int TransactionCountafterAddingNewTransaction = getTransactionResultAfterSave.bTransactions.Count();
            Assert.True(TransactionCountafterAddingNewTransaction > TransactionCountBeforeAddingNewTransaction);
            Assert.Equal(TransactionCountafterAddingNewTransaction, TransactionCountBeforeAddingNewTransaction + 1);

            var variablesData = UpdateTransaction.Variables["Transaction"];
            ((Dictionary<string, string>)variablesData)["ReferenceNumber"] = addTransactionResult.createBTransaction.referenceNumber.ToString();
            UpdateTransaction.Variables["Transaction"] = variablesData;
            var controllerUpdateResult = await _graphQLController.HandleRequest(UpdateTransaction, cancellationToken);
            Assert.NotNull(controllerUpdateResult);
            Assert.Equal(200, ((ObjectResult)controllerUpdateResult).StatusCode);
            var updateTransactionResult = JsonConvert.DeserializeObject<UpdateTransactionResponse>(JsonConvert.SerializeObject((((ObjectResult)controllerUpdateResult).Value)));
            Assert.NotNull(updateTransactionResult);
            Assert.NotNull(updateTransactionResult.updateTransaction);
            Assert.NotNull(updateTransactionResult.updateTransaction.referenceNumber);
            var updateData = ((Dictionary<string, string>)variablesData);
            Assert.Equal(updateTransactionResult.updateTransaction.referenceNumber.ToString(), updateData["referenceNumber"]);
            Assert.Equal(updateTransactionResult.updateTransaction.amount.ToString(), updateData["transactionAmount"]);
            Assert.Equal(updateTransactionResult.updateTransaction.transactionType, updateData["transactionType"]);
            //Assert.Equal(updateTransactionResult.updateTransaction.continent, updateData["continent"]);


            DeleteTransaction.Variables["ReferenceNumber"] = addTransactionResult.createBTransaction.referenceNumber;
            var controllerResult = await _graphQLController.HandleRequest(DeleteTransaction, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(200, ((ObjectResult)controllerResult).StatusCode);
            var deleteTransactionResult = JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value));
            Assert.NotNull(deleteTransactionResult);
            Assert.Contains("successfully deleted from db", deleteTransactionResult);

            var getControllerAfterDeleteResult = await _graphQLController.HandleRequest(GetTransaction, cancellationToken);
            Assert.NotNull(getControllerAfterDeleteResult);
            Assert.Equal(200, ((ObjectResult)getControllerAfterDeleteResult).StatusCode);
            var getTransactionAfterDeleteResult = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerAfterDeleteResult).Value)));
            Assert.NotNull(getTransactionAfterDeleteResult);
            int TransactionCountAfterDelete = getTransactionAfterDeleteResult.bTransactions.Count();
            Assert.Equal(TransactionCountAfterDelete, TransactionCountBeforeAddingNewTransaction);
        }

        [Theory]
        [MemberData(nameof(GraphQLModelDataForInvalidUpdate))]
        public async Task Update_Transaction_With_Invalid_Transaction_id(GraphQLModel UpdateTransaction, CancellationToken cancellationToken = default)
        {
            var controllerResult = await _graphQLController.HandleRequest(UpdateTransaction, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(400, ((ObjectResult)controllerResult).StatusCode);
            var addTransactionResult = JsonConvert.DeserializeObject<ErrorResponse>(JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value)));
            Assert.NotNull(addTransactionResult);
            Assert.NotNull(addTransactionResult.errors);
            Assert.True(addTransactionResult.errors.Any());
            Assert.Equal("Couldn't find Transaction in db.", addTransactionResult.errors[0]);
        }

        [Theory]
        [MemberData(nameof(GraphQLModelDeleteInvalidData))]
        public async Task Delete_Transaction_With_Invalid_Contry_Id(GraphQLModel DeleteTransaction, CancellationToken cancellationToken = default)
        {
            DeleteTransaction.Variables["ReferenceNumber"] = Guid.NewGuid().ToString();
            var controllerResult = await _graphQLController.HandleRequest(DeleteTransaction, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(400, ((ObjectResult)controllerResult).StatusCode);
            var deleteTransactionResult = JsonConvert.DeserializeObject<ErrorResponse>(JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value)));
            Assert.NotNull(deleteTransactionResult);
            Assert.NotNull(deleteTransactionResult.errors);
            Assert.True(deleteTransactionResult.errors.Any());
            Assert.Equal("Couldn't find Transaction in db.", deleteTransactionResult.errors[0]);
        }

        //[Theory]
        //[MemberData(nameof(GraphQLModelDataWithIsoAlreadyExist))]
        //public async Task Save_New_Transactions_With_ISOCode_Already_Exist(GraphQLModel AddTransaction, CancellationToken cancellationToken = default)
        //{
        //    var addcontrollerResult = await _graphQLController.HandleRequest(AddTransaction, cancellationToken);
        //    Assert.NotNull(addcontrollerResult);
        //    Assert.Equal(400,((ObjectResult)addcontrollerResult).StatusCode);
        //    var addTransactionResult = JsonConvert.DeserializeObject<ErrorResponse>(JsonConvert.SerializeObject((((ObjectResult)addcontrollerResult).Value)));
        //    Assert.NotNull(addTransactionResult);
        //    Assert.NotNull(addTransactionResult.errors);
        //    Assert.True(addTransactionResult.errors.Any());
        //    Assert.Equal("Transaction already exists", addTransactionResult.errors[0]);
        //}
    }
}
