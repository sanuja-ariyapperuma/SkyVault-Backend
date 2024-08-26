import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { VISAType } from "../Types/CustomerProfile/VISAType";

export interface VisaState {
  visas: VISAType[];
}

const initialState: VisaState = {
  visas: [],
};

export const visaSlice = createSlice({
  name: "VISAList",
  initialState,
  reducers: {
    replaceVISAList: (state, action: PayloadAction<VISAType[]>) => {
      if (action.payload.length === 0) {
        state.visas = [];
        return;
      }

      state.visas = action.payload;
    },
    addSingleVISA: (state, action: PayloadAction<VISAType>) => {
      state.visas.unshift(action.payload);
    },
    removeSingleVISA: (state, action: PayloadAction<string>) => {
      state.visas = state.visas.filter((visa) => visa.id !== action.payload);
    },
    updateSingle: (state, action: PayloadAction<VISAType>) => {
      let updatingVisa = state.visas.find(
        (visa) => visa.id === action.payload.id
      );
      if (updatingVisa) {
        updatingVisa.countryId = action.payload.countryId;
        updatingVisa.visaNumber = action.payload.visaNumber;
        updatingVisa.expireDate = action.payload.expireDate;
        updatingVisa.passportNumber = action.payload.passportNumber;
        updatingVisa.issuedDate = action.payload.issuedDate;
        updatingVisa.issuedPlace = action.payload.issuedPlace;
        updatingVisa.assignedToPrimaryPassport =
          action.payload.assignedToPrimaryPassport;
        updatingVisa.countryName = action.payload.countryName;
      }
    },
  },
});

export default visaSlice.reducer;
export const {
  replaceVISAList,
  addSingleVISA,
  updateSingle,
  removeSingleVISA,
} = visaSlice.actions;
