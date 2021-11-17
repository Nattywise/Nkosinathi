**Basic Bank API Test Documentation**

The API is build in ASP.Net core 3.1 using entity framework and it is using in memory database.

Open the API in Visual Studio 2019 and run it, the initial list of customers is being loaded customer table.

I recommend that you test the API using Postman application. there is a json test script under the folder \\Bank\Bank.PostMan which can be imported and it has all the HTTP Requests.

1. GET: https://localhost:44357/api/customerlist returns the list of customers;

![image](https://user-images.githubusercontent.com/94300618/142159700-fa7eab4b-e746-4054-a62a-923725923fe8.png)

2. GET:https://localhost:44357/api/customer/?id=1 returns the customer details for customer ID: 1

![image](https://user-images.githubusercontent.com/94300618/142160020-af37637d-eaf2-43f7-b94f-91b0511f7156.png)

3. GET:https://localhost:44357/api/customer/?id=99999 returns the response below if the customer does not exist;

![image](https://user-images.githubusercontent.com/94300618/142160200-2427b5a6-d96f-4bd0-8dbd-b12323c953a7.png)

4. PUST: https://localhost:44357/api/customer update the customer details and the body is required with the updated values. the response will be True if successful.

![image](https://user-images.githubusercontent.com/94300618/142160581-6008dedb-9094-4cda-9f78-2231fac2adc9.png)

5. DELETE:https://localhost:44357/api/customer/?id=5  Delete the customer specified as a parameter(5) and returns True if successful.

7. PUT: https://localhost:44357/api/customer updates the customer details with the customer ID specified on the body as below, the response will be True if successful.

![image](https://user-images.githubusercontent.com/94300618/142161488-f39518a0-edb6-4a22-84e0-a8d3a19a78d8.png)

8. POST: https://localhost:44357/api/account?initialdeposit=1000 when creatign the new account, you need to pass the initial amount and send the account details as follows. the response will be True.
  
  ![image](https://user-images.githubusercontent.com/94300618/142161881-dbdfec46-2e64-4a11-9eb1-f943a7a442dd.png)
  
9. GET: https://localhost:44357/api/accountlist Returns the list of accounts created as follows;

![image](https://user-images.githubusercontent.com/94300618/142162118-3982a5b7-e7f4-4680-a82d-eee6a8b9e60e.png)

10. GET: https://localhost:44357/api/account/?id=1 returns the account for a specified account id
 
 ![image](https://user-images.githubusercontent.com/94300618/142162365-d2916982-9e50-4aba-b0b3-e0a4bca89947.png)

11. GET: https://localhost:44357/api/accountbalance/?id=1 returns the balance for a specified account number.
 
 ![image](https://user-images.githubusercontent.com/94300618/142162551-ca2dde93-20c3-428e-bde3-f0a268f5af9e.png)

12. DELETE: https://localhost:44357/api/account/?id=1 delete the account for specified account number and returns true. 

13. PUT: https://localhost:44357/api/account updates the account details for specified account number with the following details on the body;
  
  ![image](https://user-images.githubusercontent.com/94300618/142163209-51d6184a-a1bd-489b-acee-6dac5689c50c.png)
  
14 GET: https://localhost:44357/api/transactionlist returns the list of transactions as shown below;

![image](https://user-images.githubusercontent.com/94300618/142163535-8c8e76ee-24ec-4fa2-8470-edd6a599780e.png)

15.GET: https://localhost:44357/api/transaction/?id=1 returns the transaction details for specified transaction ID

![image](https://user-images.githubusercontent.com/94300618/142163832-55390e49-fe9d-4b80-bffc-05665d344c02.png)

16. GET: https://localhost:44357/api/transactionusingaccountid/?accountid=1 returns transactions for specified account number as below;

 ![image](https://user-images.githubusercontent.com/94300618/142164018-f7b53797-8f89-4cd9-95cf-32f42b4b3595.png)
 
 17. POST: https://localhost:44357/api/transactiontransfer this will perform the transfer of funds and you need to pass the body with the details such as the two account numbers and the amount being transfer;

 ![image](https://user-images.githubusercontent.com/94300618/142165547-d94eb908-8fc2-4649-a52d-6d497f6417ab.png)
 
 18. GET: https://localhost:44357/api/transfersusingaccountid/?accountid=1 returns the list of transfers for account number 1 as below;

![image](https://user-images.githubusercontent.com/94300618/142165856-08770845-8638-4513-b75d-496ee2534182.png)
  
 19. GET: https://localhost:44357/api/transfersusingaccountid/?accountid=2 returns transfers for account number 2;

 ![image](https://user-images.githubusercontent.com/94300618/142166165-3aa5d25f-f3ad-4b89-ba47-e804b8b4ab20.png)

20. GET:https://localhost:44357/api/accountbalance/?id=1 Gets balance for account number 1 and the resonse is shown below;

![image](https://user-images.githubusercontent.com/94300618/142168772-2bc4b87c-94f9-4956-980a-7eb217ec89c3.png)

21. GET:https://localhost:44357/api/accountbalance/?id=1 Gets balance for account number 2 and the resonse is shown below;

![image](https://user-images.githubusercontent.com/94300618/142168907-08860b6b-a146-40ae-b0e8-ff59ada7458c.png)

  



