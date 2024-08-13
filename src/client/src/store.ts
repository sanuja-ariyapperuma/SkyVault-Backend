import { configureStore } from "@reduxjs/toolkit";
import { visaSlice } from "./features/reducers/VISAListReducer";

export const store = configureStore({
  reducer: { visaListR: visaSlice.reducer },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
