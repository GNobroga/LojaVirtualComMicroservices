import products from "./reduces/product";
import { configureStore } from "@reduxjs/toolkit";
import { thunk } from 'redux-thunk'

const store = configureStore({
    reducer: {
        products
    },
    middleware: getDefaultMiddleware => 
        getDefaultMiddleware().prepend(thunk)
});

export type RootState = ReturnType<typeof store.getState>;

export type AppDispatch = typeof store.dispatch;

export default store;