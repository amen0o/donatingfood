import { Restaurant } from "./restaurant";

export class Product {
    id?: string;
    title?: string;
    image?: string;
    price: number;
    restaurant?: Restaurant;
}