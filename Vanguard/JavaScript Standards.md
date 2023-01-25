> [!done] Notice
> The following is simply a style guide. It will not be enforced. These practices are simply recommended for consistency between authors

### 1. Use var instead of let
```js
var myVariable = 420
```

### 2. Prefer camelCase
Uppercase should only be used for class and top level object names
```js
someVariableName = Item.get('myItem')
```

### 3. Seperate Code Chunks by exactly 1 line
```js
Items.add( {
	"name" : "custom item",
	"type" :  "custom"
});

Console.Write('Registered Object')
```

### 4. Which Quotes should I Use?
Aim to use single quotes for strings wherever possible, the only exception being for JSON Object Literals
```js
Items.add( {
	// These strings are being used in a json object so use double quotes
	"name" : "custom item",
	"type" :  "custom"
});

// Normal String
Console.Write('Registered Object')
```

### 5. Use new lines when defining JSON Objects or Callbacks

> [!success] Do:
> ```js
> Items.add({
> 	"name": "custom item"
> });

> [!warning] Don't:
> ```js
> Items.add({"name": "custom item"})

> [!success] Do:
> ```js
> // Callback when something is purchases
> Events.OnItemPurchased( (item) => {
> 	Console.Writeline(item + 'was purchased from the shop')
> });

> [!warning] Don't:
> ```js
> // Callback when something is purchases
> Events.OnItemPurchased( (item) => { Console.Writeline(item + 'was purchased from the shop') });

### 6. The ``})`` Case
It is frequent in JS to end up with the two brackets } and ) next to eachother. In this event, where possible, we should place a semi colon at the end:
```js
// Like this:        v
myCode.Something({ });
// Not like this:    v
myCode.Something({ })
```
