import { Grid } from "@mui/material";
import { FC, useEffect, useState } from "react";
import { Product } from "../shared/types/Product";
import { ProductsApiClient } from "../../api/Clients/ProductsApiClient";
import { ProductModel } from "../../api/Models/ProductModel";
import { Category } from "../shared/types/Category";
import { CategoriesApiClient } from "../../api/Clients/CategoriesApiClients";
import { CategoryModel } from "../../api/Models/CategoryModel";
import { ProductCard } from "./ProductCard";

import "./Products.css";

export const Products: FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);

  const removeCategoryFromProduct = async (
    productId: number,
    categoryId: number
  ) => {
    try {
      if (!productId || !categoryId) return;

      const res = await CategoriesApiClient.removeCategoryToProduct(productId, categoryId);
      const removedCategory = res as Category;
      
      const previousProducts = [...products];
      const updatedProducts = previousProducts.map((product: Product) =>
        product.id === productId
          ? {
              ...product,
              categories: product.categories.filter(
                (category: string) => category !== removedCategory.name
              ),
            }
          : product
      );
      setProducts(updatedProducts)
    } catch (error: any) {
      console.log(error);
    }
  };

  const addCategoryToProduct = async (
    productId: number,
    categoryId: number
  ) => {
    try {
      if (!productId || !categoryId) return;

      const res = await CategoriesApiClient.addCategoryToProduct(
        productId,
        categoryId
      );

      const updatedProduct = res as Product;

      const updatedProductIndex = products.findIndex(
        (product) => product.id === updatedProduct.id
      );

      if (updatedProductIndex !== -1) {
        const updatedProducts = [...products];
        updatedProducts[updatedProductIndex] = updatedProduct;
        setProducts(updatedProducts);
      }
    } catch (error: any) {
      console.log(error);
    }
  };

  const fetchProducts = async () => {
    try {
      const res = await ProductsApiClient.getAllAsync();

      const products = res.map((e: ProductModel) => ({ ...e } as Product));
      setProducts(products);
    } catch (error: any) {
      console.log(error);
    }
  };

  const fetchCategories = async () => {
    try {
      const res = await CategoriesApiClient.getAllAsync();

      const categories = res.map((e: CategoryModel) => ({ ...e } as Category));
      setCategories(categories);
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchProducts();
    fetchCategories();
  }, []);

  return (
    <Grid container spacing={2} className={"products-page-container"}>
      {products.map((product: Product, index: number) => (
        <Grid item xs={3} key={`${product.id}-${index}`}>
          <ProductCard
            product={product}
            categories={categories}
            onAddingCategory={addCategoryToProduct}
            onRemovingCategory={removeCategoryFromProduct}
          />
        </Grid>
      ))}
    </Grid>
  );
};
