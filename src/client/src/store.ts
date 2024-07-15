import { configureStore } from "@reduxjs/toolkit";
import tokenSlice from "./features/reducers/tokenSlice";

export const store = configureStore({
  reducer: {
    tokenR: tokenSlice,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
