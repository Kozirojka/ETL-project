# ETL Project

This was an interesting task! I didn’t initially think the task could be anything other than a "Write Todo List on Asp.net Api" However, there was one thing I overlooked — **a null value** on 18806 line in .cvs. I didn’t account for this edge case at first, which led to some issues that took me a little time to figure out. 

🚗 Delivered

1. Try to import database was in this way: Database -> tasks -> import flat file

2. Logs from menu: 
![alt text](image.png)

    29770 - Unique records,  
    111 - Duplicate records  

3. Here is written all information about optimization of requests [SqlCommand.md](./SqlCommand.md)

4. Q: Describe in a few sentences what you would change if you knew it would be used for a 10GB CSV input file.  

    A: In my opinion, the best solution is to divide the file into smaller pieces using parallelization. Of course, this is only can be if the task allows for it.

Edited 17 March: So, I woke up, and remembered my assignmnet. So. This is how I'd Like to improve this code:
1️⃣ Instead of passing List<DbTripTransport> as a parameter, I should create a Trips class that contains a List<DbTripTransport> field.  
Then, I would pass an instance of this class as a parameter.  
This approach aligns better with OOP principles, whereas using List<> in parameters is more of a functional programming style.

It's improve scalability, incupsulation because is mean that developer do not have direct access to field of users, and readbility.

2️⃣ I should have done a small research to analyze time complexity.

Maybe later I will make changes and push it to another branch.


