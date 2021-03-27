import { Category } from "./category.model";

export class Restaurant {
    public Id: number;
    public Name: string;
    public Suburb: string;
    public LogoPath: string;
    public Rank: number;
    public Categories: Category[];
}