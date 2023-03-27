import $ from 'jquery';
import EmberFlexberryTranslations from 'ember-flexberry/locales/en/translations';

import IISNiFiSampleBrandLForm from './forms/i-i-s-ni-fi-sample-brand-l';
import IISNiFiSampleCarLForm from './forms/i-i-s-ni-fi-sample-car-l';
import IISNiFiSampleProducingCountryLForm from './forms/i-i-s-ni-fi-sample-producing-country-l';
import IISNiFiSampleBrandEForm from './forms/i-i-s-ni-fi-sample-brand-e';
import IISNiFiSampleCarEForm from './forms/i-i-s-ni-fi-sample-car-e';
import IISNiFiSampleProducingCountryEForm from './forms/i-i-s-ni-fi-sample-producing-country-e';
import IISNiFiSampleBrandModel from './models/i-i-s-ni-fi-sample-brand';
import IISNiFiSampleCarDataModel from './models/i-i-s-ni-fi-sample-car-data';
import IISNiFiSampleCarModel from './models/i-i-s-ni-fi-sample-car';
import IISNiFiSampleProducingCountryModel from './models/i-i-s-ni-fi-sample-producing-country';
import IISNiFiSampleSparePartModel from './models/i-i-s-ni-fi-sample-spare-part';

const translations = {};
$.extend(true, translations, EmberFlexberryTranslations);

$.extend(true, translations, {
  models: {
    'i-i-s-ni-fi-sample-brand': IISNiFiSampleBrandModel,
    'i-i-s-ni-fi-sample-car-data': IISNiFiSampleCarDataModel,
    'i-i-s-ni-fi-sample-car': IISNiFiSampleCarModel,
    'i-i-s-ni-fi-sample-producing-country': IISNiFiSampleProducingCountryModel,
    'i-i-s-ni-fi-sample-spare-part': IISNiFiSampleSparePartModel
  },

  'application-name': 'Apache NiFi Sample',

  forms: {
    loading: {
      'spinner-caption': 'Loading stuff, please wait for a moment...'
    },
    index: {
      greeting: 'Welcome to ember-flexberry test stand!'
    },

    application: {
      header: {
        menu: {
          'sitemap-button': {
            title: 'Menu'
          },
          'user-settings-service-checkbox': {
            caption: 'Use service to save user settings'
          },
          'show-menu': {
            caption: 'Show menu'
          },
          'hide-menu': {
            caption: 'Hide menu'
          },
          'language-dropdown': {
            caption: 'Application language',
            placeholder: 'Choose language'
          }
        },
        login: {
          caption: 'Login'
        },
        logout: {
          caption: 'Logout'
        }
      },

      footer: {
        'application-name': 'Apache NiFi Sample',
        'application-version': {
          caption: 'Addon version {{version}}',
          title: 'It is version of ember-flexberry addon, which uses in this dummy application ' +
          '(npm version + commit sha). ' +
          'Click to open commit on GitHub.'
        }
      },

      sitemap: {
        'application-name': {
          caption: 'Apache NiFi Sample',
          title: 'Apache NiFi Sample'
        },
        'application-version': {
          caption: 'Addon version {{version}}',
          title: 'It is version of ember-flexberry addon, which uses in this dummy application ' +
          '(npm version + commit sha). ' +
          'Click to open commit on GitHub.'
        },
        index: {
          caption: 'Home',
          title: ''
        },
        'ni-fi-sample': {
          caption: 'NiFiSample',
          title: 'NiFiSample',
          'i-i-s-ni-fi-sample-brand-l': {
            caption: 'Brand',
            title: ''
          },
          'i-i-s-ni-fi-sample-car-l': {
            caption: 'Car',
            title: ''
          },
          'i-i-s-ni-fi-sample-producing-country-l': {
            caption: 'Producing country',
            title: ''
          }
        },
        'add-new-data': 'Добавить тестовые данные'
      }
    },

    'edit-form': {
      'save-success-message-caption': 'Save operation succeed',
      'save-success-message': 'Object saved',
      'save-error-message-caption': 'Save operation failed',
      'delete-success-message-caption': 'Delete operation succeed',
      'delete-success-message': 'Object deleted',
      'delete-error-message-caption': 'Delete operation failed'
    },
    'i-i-s-ni-fi-sample-brand-l': IISNiFiSampleBrandLForm,
    'i-i-s-ni-fi-sample-car-l': IISNiFiSampleCarLForm,
    'i-i-s-ni-fi-sample-producing-country-l': IISNiFiSampleProducingCountryLForm,
    'i-i-s-ni-fi-sample-brand-e': IISNiFiSampleBrandEForm,
    'i-i-s-ni-fi-sample-car-e': IISNiFiSampleCarEForm,
    'i-i-s-ni-fi-sample-producing-country-e': IISNiFiSampleProducingCountryEForm
  },

});

export default translations;
