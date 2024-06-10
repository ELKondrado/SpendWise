import { format } from "date-fns";
import { SpendWiseClient } from "../Base/BaseApiClient";
import { CategoryModel } from "../Models/CategoryModel";
import { CategorySpendingModel } from "../Models/CategorySpendingModel";
import { ProductModel } from "../Models/ProductModel";

export const CategoriesApiClient = {
  urlPath: "Categories",

  getAllAsync(): Promise<CategoryModel[]> {
    return SpendWiseClient.get<CategoryModel[]>(this.urlPath).then(
      (response) => response.data
    );
  },

  getOneAsync(id: number): Promise<CategoryModel> {
    return SpendWiseClient.get<CategoryModel>(
      this.urlPath + "/GetCategory/" + id
    ).then((response) => response.data);
  },

  createOneAsync(model: CategoryModel): Promise<CategoryModel> {
    return SpendWiseClient.post<CategoryModel>(
      this.urlPath + "/CreateCategory",
      model
    ).then((response) => response.data);
  },

  updateOneAsync(model: CategoryModel): Promise<CategoryModel> {
    return SpendWiseClient.put<CategoryModel>(
      this.urlPath + "/UpdateCategory/" + model.id,
      model
    ).then((response) => response.data);
  },

  deleteOneAsync(id: number): Promise<any> {
    return SpendWiseClient.delete(this.urlPath + "/DeleteCategory/" + id).then(
      (response) => response.data
    );
  },

  getSpendingAsync(startDate: Date | null, endDate: Date | null): Promise<CategorySpendingModel[]> {
    const dateFrom = startDate ? format(startDate, 'yyyy-MM-dd\'T\'HH:mm:ss') : null;
    const dateTo = endDate ? format(endDate, 'yyyy-MM-dd\'T\'HH:mm:ss') : null;

    const params = new URLSearchParams();
    if (dateFrom) {
      params.append('dateFrom', dateFrom);
    }
    if (dateTo) {
      params.append('dateTo', dateTo);
    }

    return SpendWiseClient.get<CategorySpendingModel[]>(this.urlPath + "/GetCategoriesSpending?" + params.toString()).then(
      (response) => response.data
    );
  },

  addCategoryToProduct(productId: number, categoryId: number): Promise<ProductModel> {
    return SpendWiseClient.post<ProductModel>(this.urlPath + "/AddCategoryToProduct/" + productId + "/" + categoryId).then(
      (response) => response.data
    );
  },

  removeCategoryToProduct(productId: number, categoryId: number): Promise<CategoryModel> {
    return SpendWiseClient.delete<CategoryModel>(this.urlPath + "/RemoveCategoryToProduct/" + productId + "/" + categoryId).then(
      (response) => response.data
    );
  }
};
