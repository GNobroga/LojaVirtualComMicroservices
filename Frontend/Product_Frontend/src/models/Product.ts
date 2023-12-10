export interface Product {
    id: number;
    name: string;
    price: number;
    description: string | null;
    stock: number;
    imageUrl: string | null;
    categoriaName: string | null;
    categoryId: number;
}