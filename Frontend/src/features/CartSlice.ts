import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "../app/store";
import { ICartItem } from "../interfaces/ICartItem";
import { isItemInCart, isCartItemEqual } from "./SliceHelpers";

// Define a type for the slice state
interface CartState {
  items: ICartItem[];
}

const initialState: CartState = {
  items: [],
};

export const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart: (state, action: PayloadAction<ICartItem>) => {
      const itemExistsInCart = isItemInCart(state.items, action.payload);

      if (!itemExistsInCart) {
        state.items.push(action.payload);
      }
    },
    removeFromCart: (state, action: PayloadAction<ICartItem>) => {
      state.items = state.items.filter(
        (cartItem) => !isCartItemEqual(cartItem, action.payload)
      );
    },
    clearCart: (state) => {
      state.items = [];
    },
  },
});

export const { addToCart, removeFromCart, clearCart } = cartSlice.actions;

export const selectItems = (state: RootState) => state.cart.items;
export const selectItemsCountCount = (state: RootState) =>
  state.cart.items.length;

export default cartSlice.reducer;
