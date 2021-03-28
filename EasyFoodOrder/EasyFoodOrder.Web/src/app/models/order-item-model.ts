export class OrderItem {
    public restaurantId: number;
    public menuItemId: number;
    public price: number;

    constructor (restaurantId: number, menuItemId: number, price: number) {
        this.restaurantId = restaurantId;
        this.menuItemId = menuItemId;
        this.price = price;
    }
}