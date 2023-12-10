import React from 'react';
import { BrowserRouter, Route, Routes } from "react-router-dom";

const Product = React.lazy(() => import('./components/products/Products'));
const CreateProduct = React.lazy(() => import('./components/create-product/CreateProduct'));

function App() {
  return (
   <BrowserRouter>
    <Routes>
      <Route path="/" element={
       <React.Suspense >
          <Product/>
       </React.Suspense>
      } />
      <Route path="/create-product" element={
       <React.Suspense >
          <CreateProduct/>
       </React.Suspense>
      } />
    </Routes>
   </BrowserRouter>
  );
}

export default App;