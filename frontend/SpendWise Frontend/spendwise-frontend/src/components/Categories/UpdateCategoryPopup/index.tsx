import { FC, useState, useEffect } from "react";
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
} from "@mui/material";
import { CategoryModel } from "../../../api/Models/CategoryModel";
import { CategoriesApiClient } from "../../../api/Clients/CategoriesApiClients";
import { Category } from "../../shared/types/Category";

import "./UpdateCategoryPopup.css";

interface UpdateCategoryPopUpProps {
  open: boolean;
  onClose: () => void;
  onEditing: (category: Category) => void;
  category: Category | null;
}

export const UpdateCategoryPopUp: FC<UpdateCategoryPopUpProps> = ({
  open,
  onClose,
  onEditing,
  category,
}) => {
  const [categoryName, setCategoryName] = useState("");

  useEffect(() => {
    if (category) {
      setCategoryName(category.name);
    }
  }, [category]);

  const updateCategory = async () => {
    if (!category) return;
    const model: CategoryModel = { id: category.id, name: categoryName };

    try {
      if (!model.id) return;
      const res = await CategoriesApiClient.updateOneAsync(model);
      return res;
    } catch (error: any) {
      console.log(error);
    }
  };

  const handleClose = () => {
    onClose();
  };

  const handleSave = async () => {
    const categoryModel = await updateCategory();
    const updatedCategory = categoryModel as Category;
    onEditing(updatedCategory);
    handleClose();
  };

  return (
    <Dialog fullWidth={true} maxWidth={"md"} open={open} onClose={onClose}>
      <DialogTitle fontSize={24}>Update category</DialogTitle>
      <DialogContent className={"update-category-modal-content"}>
        <TextField
          fullWidth
          label="Category Name"
          value={categoryName}
          onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
            setCategoryName(event.target.value);
          }}
        />
      </DialogContent>
      <DialogActions className={"add-category-modal-actions"}>
        <Button onClick={handleClose} variant="outlined">
          Close
        </Button>
        <Button
          onClick={handleSave}
          variant="contained"
          disabled={!categoryName || category?.name === categoryName}
          className="update-button"
        >
          Update
        </Button>
      </DialogActions>
    </Dialog>
  );
};
