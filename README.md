# DFM — Data Function Manipulation 

Data Function Manipulation (Or DFM) is a language based on functions that can be compiled into SQL. The intent of this language is to create a safer version of SQL, since SQL can be brutal with a few missing keywords, DFM would ensure that doesnt happen.


# Idea of DFM

```[ChangeLog.md](https://github.com/user-attachments/files/26168051/ChangeLog.md)

/*DFM (Data Function Maninpulation)*/

// Database Name
CreateDatabase(myDatabase);

// Database Name
Use(myDatabase);

// TableName - Columns
CreateTable(myTable: [PK] int id, string username);
// All Types: String, Char, Text (Just here for fun), Int, Float, Double, Number (Allows all numbers)


// Table - Values
Insert(myTable: Set(1, "Dog"), Set(2, "Cat"));

// Table - Values to change - Condition
Update(myTable: username = "BANNED", id = 0: id > 10);

//  Table - Column Type + Name - Default Value
AddColumn(myTable: string Nickname: "User");

//  Table - ColumnName
RemoveColumn(myTable: Nickname);

// Table - Rows - Condition (Optional) - Order By (Optional)
Select(myTable: * : id % 2 == 0 : ASC);

// Table - Condition (Required)
DeleteRows(myTable: username = "BANNED");

// Table
ClearTable(myTable);

// Table
DeleteTable(myTable);

// Database
DeleteDatabase(myDatabase);
```

Sadly as of now, there is only a `SELECT()` function but hopefully that can be changed.
> DFM is intentionally incomplete
