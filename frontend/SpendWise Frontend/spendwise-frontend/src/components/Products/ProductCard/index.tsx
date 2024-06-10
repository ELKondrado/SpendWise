/* eslint-disable react-hooks/exhaustive-deps */
import { FC, useEffect, useState } from "react";
import { Category } from "../../shared/types/Category";
import {
  Box,
  Typography,
  Card,
  CardContent,
  Select,
  MenuItem,
  SelectChangeEvent,
} from "@mui/material";
import { Product } from "../../shared/types/Product";

import "./ProductCard.css";

interface ProductCardProps {
  product: Product;
  categories: Category[];
  onAddingCategory: (productId: number, categoryId: number) => void;
  onRemovingCategory: (productId: number, categoryId: number) => void;
}

export const ProductCard: FC<ProductCardProps> = ({
  product,
  categories,
  onAddingCategory,
  onRemovingCategory,
}: ProductCardProps) => {
  const [categoriesToAdd, setCategoriesToAdd] = useState<Category[]>([]);
  const [categoriesToRemove, setCategoriesToRemove] = useState<Category[]>([]);

  useEffect(() => {
    if (categories.length > 0) {
      setCategoriesToAdd(fetchCategoriesToAdd(product));
      setCategoriesToRemove(fetchCategoriesToRemove(product));
    }
  }, [categories, product]);

  const fetchCategoriesToAdd = (product: Product) => {
    const filteredCategories = categories.filter(
      (category1) =>
        !product.categories.some((category2) => category1.name === category2)
    );
    return filteredCategories;
  };

  const fetchCategoriesToRemove = (product: Product) => {
    const filteredCategories = categories.filter((category1) =>
      product.categories.some((category2) => category1.name === category2)
    );
    return filteredCategories;
  };

  const handleAddCategory = (event: SelectChangeEvent<number>) => {
    onAddingCategory(product.id, event.target.value as number);
  };

  const handleRemoveCategory = (event: SelectChangeEvent<number>) => {
    onRemovingCategory(product.id, event.target.value as number);
  };

  return (
    <Card sx={{ padding: "0px" }} className={"products-page-card"}>
      <CardContent sx={{ padding: "0px" }}>
        <Typography
          variant="h6"
          component="div"
          className={"products-page-category"}
        >
          {product.name}
        </Typography>
        <Typography
          variant="h6"
          component="div"
          className={"products-page-categories"}
        >
          Category: {product.categories.join(", ")}
        </Typography>
        <Box className={"products-category-operations"}>
          <Select
            value={-1}
            onChange={handleRemoveCategory}
            className={"category-select"}
          >
            <MenuItem disabled value={-1}>
              Remove Category
            </MenuItem>
            {categoriesToRemove.map((category) => (
              <MenuItem key={category.id} value={category.id}>
                {category.name}
              </MenuItem>
            ))}
          </Select>

          <Select
            value={-1}
            onChange={handleAddCategory}
            className={"category-select"}
          >
            <MenuItem disabled value={-1}>
              Add Category
            </MenuItem>
            {categoriesToAdd.map((category) => (
              <MenuItem key={category.id} value={category.id}>
                {category.name}
              </MenuItem>
            ))}
          </Select>
        </Box>
      </CardContent>
    </Card>
  );
};
