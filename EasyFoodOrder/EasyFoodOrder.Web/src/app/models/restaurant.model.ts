import { Category } from "./category.model";

export class Restaurant {
    public id: number;
    public name: string;
    public city: string;
    public suburb: string;
    public logoPath: string;
    public rank: number;
    public categories: Category[];
}