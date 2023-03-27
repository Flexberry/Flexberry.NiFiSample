import $ from 'jquery';
import EmberFlexberryTranslations from 'ember-flexberry/locales/ru/translations';

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
      'spinner-caption': 'Данные загружаются, пожалуйста подождите...'
    },
    index: {
      greeting: 'Добро пожаловать на тестовый стенд ember-flexberry!'
    },

    application: {
      header: {
        menu: {
          'sitemap-button': {
            title: 'Меню'
          },
          'user-settings-service-checkbox': {
            caption: 'Использовать сервис сохранения пользовательских настроек'
          },
          'show-menu': {
            caption: 'Показать меню'
          },
          'hide-menu': {
            caption: 'Скрыть меню'
          },
          'language-dropdown': {
            caption: 'Язык приложения',
            placeholder: 'Выберите язык'
          }
        },
        login: {
          caption: 'Вход'
        },
        logout: {
          caption: 'Выход'
        }
      },

      footer: {
        'application-name': 'Apache NiFi Sample',
        'application-version': {
          caption: 'Версия аддона {{version}}',
          title: 'Это версия аддона ember-flexberry, которая сейчас используется в этом тестовом приложении ' +
          '(версия npm-пакета + хэш коммита). ' +
          'Кликните, чтобы перейти на GitHub.'
        }
      },

      sitemap: {
        'application-name': {
          caption: 'Apache NiFi Sample',
          title: 'Apache NiFi Sample'
        },
        'application-version': {
          caption: 'Версия аддона {{version}}',
          title: 'Это версия аддона ember-flexberry, которая сейчас используется в этом тестовом приложении ' +
          '(версия npm-пакета + хэш коммита). ' +
          'Кликните, чтобы перейти на GitHub.'
        },
        index: {
          caption: 'Главная',
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
      'save-success-message-caption': 'Сохранение завершилось успешно',
      'save-success-message': 'Объект сохранен',
      'save-error-message-caption': 'Ошибка сохранения',
      'delete-success-message-caption': 'Удаление завершилось успешно',
      'delete-success-message': 'Объект удален',
      'delete-error-message-caption': 'Ошибка удаления'
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
