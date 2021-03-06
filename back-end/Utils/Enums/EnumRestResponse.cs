﻿using System.ComponentModel;

namespace SEPractices4ML.Utils.Enums
{
    public enum RestResponse
    {
        SUCCESS = 200,
        NOT_FOUND = 404,
        SERVER_ERROR = 500,
        ERROR_USUARIO_JA_CADASTRADO_COM_FACEBOOK = 1,
        ERROR_USUARIO_JA_CADASTRADO_COM_GOOGLE = 2,
        ERROR_EMAIL_JA_CADASTRADO = 3,
        ERROR_USERNAME_JA_CADASTRADO = 4,
        USUARIO_AGUARDANDO_LIBERACAO_DO_ADMINISTRADOR = 5
    }
}
