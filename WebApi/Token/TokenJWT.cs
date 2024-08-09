﻿using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Token
{
    public class TokenJWT
    {
        private JwtSecurityToken _token;

        internal TokenJWT(JwtSecurityToken token)
        {
            this._token = token;
        }

        public DateTime ValidTo => _token.ValidTo;

        public string Value => new JwtSecurityTokenHandler().WriteToken(this._token);
    }
}