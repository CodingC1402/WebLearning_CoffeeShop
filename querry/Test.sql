use coffee_shop;
select * from `OrderDetails` inner join `Coffees` on OrderDetails.CoffeeId = Coffees.Id where OrderId = 512;
select sum(OrderDetails.Count * Coffees.Price) from `OrderDetails` inner join `Coffees` on OrderDetails.CoffeeId = Coffees.Id where OrderId = 504;