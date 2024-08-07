CREATE TABLE "Customers" (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "Email" VARCHAR(200) NOT NULL,
    "Address" VARCHAR(255) NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP
);

CREATE TABLE "Products" (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP
);

CREATE TABLE "Billings" (
    "Id" UUID PRIMARY KEY,
    "CustomerId" UUID NOT NULL,
    "InvoiceNumber" VARCHAR(50) NOT NULL UNIQUE,
    "Date" TIMESTAMP NOT NULL,
    "DueDate" TIMESTAMP NOT NULL,
    "TotalAmount" DECIMAL(18, 2) NOT NULL,
    "Currency" VARCHAR(5) NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP,

    CONSTRAINT "FK_Customer"
      FOREIGN KEY("CustomerId")
	  REFERENCES "Customers"("Id")
);

CREATE UNIQUE INDEX "IX_InvoiceNumber" ON "Billings" ("InvoiceNumber");

CREATE TABLE "BillingLines" (
    "Id" UUID PRIMARY KEY,
    "BillingId" UUID NOT NULL,
    "ProductId" UUID NOT NULL,
    "Quantity" DECIMAL(18, 2) NOT NULL,
    "UnitPrice" DECIMAL(18, 2) NOT NULL,
    "Subtotal" DECIMAL(18, 2) NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP,

    CONSTRAINT "FK_Billings"
      FOREIGN KEY("BillingId")
	  REFERENCES "Billings"("Id"),

    CONSTRAINT "FK_Product"
      FOREIGN KEY("ProductId")
	  REFERENCES "Products"("Id")
);
