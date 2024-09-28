import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AuthenticatedResponse } from "../Types/Login/authenticatedResponse";

export interface UserState {
  user: AuthenticatedResponse;
}

const initialState: UserState = {
  user: {
    fullName: "",
    userRole: "",
  },
};

export const userContextSlice = createSlice({
  name: "UserContext",
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<AuthenticatedResponse>) => {
      state.user = action.payload;
    },
    clearUser: (state) => {
      state.user = {
        fullName: "",
        userRole: "",
      };
    },
  },
});

export default userContextSlice.reducer;
export const { setUser, clearUser } = userContextSlice.actions;
