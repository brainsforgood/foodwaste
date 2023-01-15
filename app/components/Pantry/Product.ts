export class Product {
    id: number;
    name: string;
    expiryDate: Date;

    constructor(id: number, name: string, expiryDate: Date = new Date()) {
        this.id = id;
        this.name = name;
        this.expiryDate = expiryDate;
    }
}