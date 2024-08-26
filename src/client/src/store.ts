import { configureStore } from "@reduxjs/toolkit";
import { visaSlice } from "./features/reducers/VISAListReducer";
import { userContextSlice } from "./features/reducers/UserContextSliceReducer";

export const store = configureStore({
  reducer: {
    visaListR: visaSlice.reducer,
    userContextR: userContextSlice.reducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
