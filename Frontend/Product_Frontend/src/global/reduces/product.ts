import { createSlice } from "@reduxjs/toolkit";
import { AppDispatch } from "../store";
import { Product } from "../../models/Product";
import { Category } from "../../models/Category";

const slice = createSlice({
    name: 'product',
    initialState: [] as Product[],
    reducers: {
       fetchAll(state, action) {
        return action.payload;
       } 
    }
});

export const findAllProducts = () => async (dispatch: AppDispatch) => {
    const req = await fetch('http://localhost:5021/api/products', { cache: 'no-cache' });
    const res = await req.json();
    dispatch(fetchAll(res as Product[]));
};

export const findById = (id: number) => async (dispatch: AppDispatch) => {
    const req = await fetch(`http://localhost:5021/api/products/${id}`, { cache: 'no-cache' });
    return await req.json();
};


export const createProduct = (record: Product) => async (dispatch: AppDispatch) => {
    const req = await fetch('http://localhost:5021/api/products', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(record)
    });
    if (!req.ok) {
        console.log(await req.json());
    }
    dispatch(findAllProducts());
};

export const deleteProduct = (id: number) => async (dispatch: AppDispatch) => {
    await fetch(`http://localhost:5021/api/products/${id}`, {
        method: 'delete'
    });
    dispatch(findAllProducts());
};

export const updateProduct = (record: Partial<Product>) => async (dispatch: AppDispatch) => {
    await fetch(`http://localhost:5021/api/products/${record.id}`, {
        method: 'put',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(record)
    });
    dispatch(findAllProducts());
};

export const getCategories = () => async (dispatch: AppDispatch) => {
    const req = await fetch('http://localhost:5021/api/categories');
    return (await req.json()) as Category[];
};

export const { fetchAll } = slice.actions;

export default slice.reducer;

