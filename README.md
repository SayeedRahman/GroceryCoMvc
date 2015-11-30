# GroceryCoMvc
A demonstration of Pricing-Rules based Check-Out system

Assumptions:
1. The "list of single items" in the ShoppingCart is created from a list of products. Therefore a  Product class is created.

2. To be able to produce the ShoppingCard, the " unsorted list of single items " is assumed to contain a "ProductId"  for each selected item by the shopper.

3. Pricing Rules are implemented in the following manner:
	3.a ) During the checkout process, a PricingRule# is entered for a 		selected item/product. PricingRule# may conform to some numbering system,

	3.b) The PricingRule returns the discount in 'set dollar amount' or 'calculated dollar 	amount'. For example, if a PricingRule contains "25% discount", it returns $2.50 for 	the price of $10 of an item, and another contains "Buy 3 for $2.00" will return $2.00.
