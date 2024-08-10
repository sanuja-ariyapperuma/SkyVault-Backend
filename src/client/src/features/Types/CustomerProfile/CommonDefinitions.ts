export type Salutation = {
  id: string;
  name: string;
};

export type Nationality = {
  id: string;
  name: string;
};

export type Gender = {
  id: string;
  name: string;
};

export type Country = {
  id: string;
  name: string;
  countrycode: string;
};

export type ProfileDefinitionResponse = {
  salutations: Salutation[];
  nationalities: Nationality[];
  genders: Gender[];
  countries: Country[];
};
